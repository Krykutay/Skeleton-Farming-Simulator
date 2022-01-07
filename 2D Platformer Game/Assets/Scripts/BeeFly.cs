using System;
using System.Collections;
using UnityEngine;

public class BeeFly : MonoBehaviour, IDamageable
{
    [SerializeField] float _moveSpeed = 5f;
    [SerializeField] float _turnTime = 3f;
    [SerializeField] float _spawnDuration = 8f;

    SpriteRenderer _spriteRenderer;
    CircleCollider2D _collider;
    Animator _anim;

    float _heightVariance;
    float _initialTurnTime;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<CircleCollider2D>();
        _anim = GetComponent<Animator>();
    }

    void Start()
    {
        _heightVariance = UnityEngine.Random.Range(0.4f, 1.2f);
        _initialTurnTime = _turnTime;
    }

    void Update()
    {
        _turnTime -= Time.deltaTime;
        if (_turnTime < 0)
        {
            _turnTime = _initialTurnTime;
            _moveSpeed = -_moveSpeed;
            transform.Rotate(0f, 180f, 0f);
        }

        transform.position += new Vector3(_moveSpeed * Time.deltaTime, Mathf.Sin(Time.time) * Time.deltaTime * _heightVariance);
    }

    public bool Damage(AttackDetails attackDetails)
    {
        DeathBloodParticlePool.Instance.Get(transform.position, Quaternion.identity);
        DeathChunkParticlePool.Instance.Get(transform.position, Quaternion.identity);

        _anim.enabled = false;
        _spriteRenderer.enabled = false;
        _collider.enabled = false;

        StartCoroutine(BeeDied());
        return true;
    }

    IEnumerator BeeDied()
    {
        yield return new WaitForSeconds(_spawnDuration);

        _anim.enabled = true;
        _spriteRenderer.enabled = true;
        _collider.enabled = true;
    }

}
