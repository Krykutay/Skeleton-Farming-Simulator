using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileArrow : Projectile
{
    [SerializeField] float _gravity;

    bool isGravityOn;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        rb.gravityScale = 0f;

        xStartPosition = transform.position.x;
        isGravityOn = false;
    }

    void FixedUpdate()
    {
        if (isGravityOn)
        {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        if (Mathf.Abs(xStartPosition - transform.position.x) >= travelDistance && !isGravityOn && !hasHitGround)
        {
            hasHitGround = false;
            isGravityOn = true;
            rb.gravityScale = _gravity;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            attackDetails.position = transform.position;

            if (collision.TryGetComponent<IDamageable>(out var damageable))
            {
                bool isHit = damageable.Damage(attackDetails);

                if (isHit)
                {
                    rb.velocity = Vector2.zero;
                    EnemyArrowPool.Instance.ReturnToPool(this);
                }
            }
        }
        else
        {
            hasHitGround = true;
            rb.gravityScale = 0f;
            rb.velocity = Vector2.zero;
            StartCoroutine(DisableProjectile(projectileDurationAfterHitGround));
        }
    }

    IEnumerator DisableProjectile(float duration)
    {
        yield return new WaitForSeconds(duration);
        EnemyArrowPool.Instance.ReturnToPool(this);
    }
}
