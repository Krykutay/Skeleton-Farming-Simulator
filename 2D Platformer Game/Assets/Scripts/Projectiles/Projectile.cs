using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected float _projectileDurationAfterHitGround = 0.3f;

    protected Rigidbody2D rb;
    protected Transform playerTransform;

    protected AttackDetails attackDetails;

    protected float travelDistance;
    protected float xStartPosition;

    protected bool hasHitGround;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerTransform = Player.Instance.transform.Find("Core").transform;
    }

    protected virtual void OnEnable()
    {
        attackDetails.position = transform.position;
    }
    
    public void FireProjectile(float speed, float travelDistance, float damage)
    {
        Vector3 firePosition = (playerTransform.position - transform.position).normalized;
        rb.velocity = firePosition * speed;

        this.travelDistance = travelDistance;
        attackDetails.damageAmount = damage;
    }

}
