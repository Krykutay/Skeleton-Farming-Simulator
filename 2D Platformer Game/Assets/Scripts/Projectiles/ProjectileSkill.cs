using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSkill : Projectile
{
    Animator _anim;
    Collider2D _collider;

    Vector3 _fireDirection;

    bool _isCasting;

    protected override void Awake()
    {
        base.Awake();

        _anim = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        _fireDirection = (playerTransform.position - transform.position).normalized;
        hasHitGround = false;
        _isCasting = true;
        startPosition = transform.position;
        _anim.SetBool("isCasting", _isCasting);
    }

    private void OnDisable()
    {
        _collider.enabled = false;
    }

    void FixedUpdate()
    {
        if (Vector3.Distance(startPosition, transform.position) >= travelDistance && !hasHitGround)
        {
            EnemySkillPool.Instance.ReturnToPool(this);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        attackDetails.position = transform.position;

        if (collision.CompareTag("Player"))
        {
            bool isHit = Player.Instance.Damage(attackDetails);

            if (isHit)
            {
                rb.velocity = Vector2.zero;
                EnemySkillPool.Instance.ReturnToPool(this);
            }
        }
        else
        {
            hasHitGround = true;
            rb.velocity = Vector2.zero;
            if (gameObject.activeSelf)
                StartCoroutine(DisableProjectile(projectileDurationAfterHitGround));
        }
    }

    IEnumerator DisableProjectile(float duration)
    {
        yield return new WaitForSeconds(duration);
        EnemySkillPool.Instance.ReturnToPool(this);
    }

    public override void FireProjectile(float speed, float travelDistance, float damage)
    {
        rb.velocity = _fireDirection * speed;
        this.travelDistance = travelDistance;
        attackDetails.damageAmount = damage;

        _isCasting = false;
        _anim.SetBool("isCasting", _isCasting);
        _collider.enabled = true;
    }

}
