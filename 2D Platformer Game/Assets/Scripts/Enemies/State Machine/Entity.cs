using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public FiniteStateMachine stateMachine;

    public D_Entity entityData;

    public int facingDirection { get; private set; }

    public GameObject aliveGO { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }

    [SerializeField] Transform _wallCheck;
    [SerializeField] Transform _ledgeCheck;
    [SerializeField] Transform _playerCheck;

    Vector2 _velocityWorkspace;

    public virtual void Awake()
    {
        aliveGO = transform.Find("Alive").gameObject;
        rb = aliveGO.GetComponent<Rigidbody2D>();
        anim = aliveGO.GetComponent<Animator>();

        stateMachine = new FiniteStateMachine();
    }

    public virtual void Start()
    {
        facingDirection = 1;
    }

    public virtual void Update()
    {
        stateMachine.currentState.LogicUpdate();
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    public virtual void SetVelocity(float velocity)
    {
        _velocityWorkspace.Set(facingDirection * velocity, rb.velocity.y);
        rb.velocity = _velocityWorkspace;
    }

    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(_wallCheck.position, aliveGO.transform.right, entityData.wallCheckDistance, entityData.ground);
    }

    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(_ledgeCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.ground);
    }

    public virtual void Flip()
    {
        facingDirection *= -1;
        aliveGO.transform.Rotate(0f, 180f, 0f);
    }

    public virtual bool CheckPlayerInMinAgroRange()
    {
        return Physics2D.Raycast(_playerCheck.position, aliveGO.transform.right, entityData.minAgroDistance, entityData.player);
    }
    
    public virtual bool CheckPlayerInMaxAgroRange()
    {
        return Physics2D.Raycast(_playerCheck.position, aliveGO.transform.right, entityData.maxAgroDistance, entityData.player);
    }

    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(_wallCheck.position, _wallCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.wallCheckDistance));
        Gizmos.DrawLine(_ledgeCheck.position, _ledgeCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));
    }

}
