using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float _projectileDurationAfterHitGround = 1f;

    protected Rigidbody2D rb;
    protected Transform playerTransform;

    protected AttackDetails attackDetails;

    protected float travelDistance = Mathf.Infinity;
    protected Vector2 startPosition;

    protected bool hasHitGround;

    protected float projectileDurationAfterHitGround { get { return _projectileDurationAfterHitGround; } }

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerTransform = Player.Instance.transform.Find("Core").Find("PlayerHitPosition").transform;
    }

    protected virtual void OnEnable()
    {
        attackDetails.position = transform.position;
    }
    
    public virtual void FireProjectile(float speed, float travelDistance, float damage)
    {
        Vector3 firePosition = (playerTransform.position - transform.position).normalized;
        rb.velocity = firePosition * speed;

        this.travelDistance = travelDistance;
        attackDetails.damageAmount = damage;
    }

}
