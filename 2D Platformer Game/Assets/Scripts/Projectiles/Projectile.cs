using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float _gravity;
    [SerializeField] float _damageRadius;
    [SerializeField] float _projectileDurationAfterHitGround = 0.3f;

    [SerializeField] LayerMask _ground;
    [SerializeField] LayerMask _player;

    [SerializeField] Transform _damagePosition;

    AttackDetails _attackDetails;

    float _travelDistance;
    float _xStartPosition;

    Rigidbody2D _rb;
    Transform _playerTransform;

    bool isGravityOn;
    bool hasHitGround;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerTransform = Player.Instance.transform.Find("Core").transform;
    }

    void OnEnable()
    {
        _rb.gravityScale = 0f;

        _xStartPosition = transform.position.x;
        isGravityOn = false;
    }

    void Update()
    {
        if (!hasHitGround)
        {
            _attackDetails.position = transform.position;
            //_attackDetails.position += Vector2.right * Time.deltaTime * _speed;

            if (isGravityOn)
            {
                float angle = Mathf.Atan2(_rb.velocity.y, _rb.velocity.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }

    void FixedUpdate()
    {
        Collider2D damageHit = Physics2D.OverlapCircle(_damagePosition.position, _damageRadius, _player);
        Collider2D groundHit = Physics2D.OverlapCircle(_damagePosition.position, _damageRadius, _ground);

        if (damageHit)
        {
            if (damageHit.TryGetComponent<IDamageable>(out var damageable))
            {
                bool isHit = damageable.Damage(_attackDetails);

                if (isHit)
                    EnemyArrowPool.Instance.ReturnToPool(this);
            }
            
        }
        else if (groundHit)
        {
            hasHitGround = true;
            _rb.gravityScale = 0f;
            _rb.velocity = Vector2.zero;
            StartCoroutine(DisableProjectile(_projectileDurationAfterHitGround));
        }

        if (!hasHitGround)
        {
            if (Mathf.Abs(_xStartPosition - transform.position.x) >= _travelDistance && !isGravityOn)
            {
                isGravityOn = true;
                _rb.gravityScale = _gravity;
            }
        }
    }

    IEnumerator DisableProjectile(float duration)
    {
        yield return new WaitForSeconds(duration);
        EnemyArrowPool.Instance.ReturnToPool(this);
    }

    public void FireProjectile(float speed, float travelDistance, float damage)
    {
        Vector3 firePosition = (_playerTransform.position - transform.position).normalized;
        _rb.velocity = firePosition * speed;

        this._travelDistance = travelDistance;
        _attackDetails.damageAmount = damage;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_damagePosition.position, _damageRadius);
    }
}
