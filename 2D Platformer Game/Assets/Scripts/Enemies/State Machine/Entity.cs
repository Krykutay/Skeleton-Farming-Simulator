using UnityEngine;

public class Entity : MonoBehaviour, IDamageable
{
    public FiniteStateMachine stateMachine;

    public D_Entity entityData;

    public int facingDirection { get; private set; }
    public int lastDamageDirection { get; private set; }

    public Transform playerTransform { get; private set; }

    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }
    public AnimationToStateMachine atsm { get; private set; }

    protected bool isStunned;
    protected bool isDead;

    [SerializeField] Transform _groundCheck;
    [SerializeField] Transform _wallCheck;
    [SerializeField] Transform _ledgeCheck;
    [SerializeField] Transform _playerCheck;

    Vector2 _velocityWorkspace;

    float _currentHealth;
    float _currentStunResistance;
    float _lastDamagetime;

    public virtual void Awake()
    {
        playerTransform = Player.Instance.transform.Find("Core").Find("PlayerHitPosition").transform;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        atsm = GetComponent<AnimationToStateMachine>();

        stateMachine = new FiniteStateMachine();
    }

    public virtual void OnEnable()
    {
        _currentHealth = entityData.maxHealth;
        _currentStunResistance = entityData.stunResistance;
        facingDirection = 1;
        isStunned = false;
        isDead = false;
    }

    public virtual void Start()
    {

    }

    public virtual void Update()
    {
        stateMachine.currentState.LogicUpdate();

        //anim.SetFloat("yVelocity", rb.velocity.y);

        if (Time.time >= _lastDamagetime + entityData.stunRecoveryTime)
        {
            ResetStunResistance();
        }
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    public virtual void SetVelocityX(float velocity)
    {
        _velocityWorkspace.Set(facingDirection * velocity, rb.velocity.y);
        rb.velocity = _velocityWorkspace;
    }

    public virtual void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        _velocityWorkspace.Set(angle.x * velocity * direction, angle.y * velocity);
        rb.velocity = _velocityWorkspace;
    }

    public virtual void ResetStunResistance()
    {
        isStunned = false;
        _currentStunResistance = entityData.stunResistance;
    }

    public virtual bool Damage(AttackDetails attackDetails)
    {
        if (isDead)
            return true;

        _lastDamagetime = Time.time;

        _currentHealth -= attackDetails.damageAmount;
        _currentStunResistance -= attackDetails.stunDamageAmount;

        DamageHop(entityData.damageHopSpeed);

        if (attackDetails.position.x > transform.position.x)
        {
            lastDamageDirection = -1;
        }
        else
        {
            lastDamageDirection = 1;
        }

        if (_currentStunResistance <= 0)
        {
            isStunned = true;
        }

        if (_currentHealth <= 0)
        {
            isDead = true;
        }

        return true;
    }

    public virtual void DamageHop(float velocity)
    {
        _velocityWorkspace.Set(rb.velocity.x, velocity);
        rb.velocity = _velocityWorkspace;
    }

    public virtual void Flip()
    {
        facingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
    }

    public virtual void CheckIfShouldFlip()
    {
        float playerDirectionOnX = playerTransform.position.x - transform.position.x;

        if (playerDirectionOnX > 0f && facingDirection == -1)
            Flip();
        else if (playerDirectionOnX < 0f && facingDirection == 1)
            Flip();
    }

    public virtual bool CheckGround()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, entityData.groundCheckRadius, entityData.ground);
    }

    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(_wallCheck.position, transform.right, entityData.wallCheckDistance, entityData.ground);
    }

    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(_ledgeCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.ground);
    }

    public virtual bool CheckLedgeBehind()
    {
        return false;
    }

    public virtual bool CheckPlayerInMinAgroRange()
    {
        float distance = Vector2.Distance(_playerCheck.position, playerTransform.position);

        if (distance > entityData.minAgroDistance)
            return false;

        return !Physics2D.Raycast(_playerCheck.position, (playerTransform.position - _playerCheck.position).normalized, distance, entityData.ground);
    }
    
    public virtual bool CheckPlayerInMaxAgroRange()
    {
        float distance = Vector2.Distance(_playerCheck.position, playerTransform.position);

        if (distance > entityData.maxAgroDistance)
            return false;

        return !Physics2D.Raycast(_playerCheck.position, (playerTransform.position - _playerCheck.position).normalized, distance, entityData.ground);
    }

    public virtual bool CheckPlayerInMeleeRangeAction()
    {
        float distance = Vector2.Distance(_playerCheck.position, playerTransform.position);

        if (distance > entityData.meleeRangeActionDistance)
            return false;

        return !Physics2D.Raycast(_playerCheck.position, (playerTransform.position - _playerCheck.position).normalized, distance, entityData.ground);
    }

    public virtual bool CheckIfPlayerReachableByMeleeAction()
    {
        float distance = Mathf.Abs(_playerCheck.position.y - playerTransform.position.y);

        return entityData.meleeRangeActionDistance > distance + 0.1f;

    }

    public virtual void RotateBodyToPlayer()
    {

    }

    public virtual void ResetBodyPosition()
    {

    }

    public virtual void Respawned()
    {
        _currentHealth = entityData.maxHealth;
        _currentStunResistance = entityData.stunResistance;
        isStunned = false;
        isDead = false;
    }

    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(_wallCheck.position, _wallCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.wallCheckDistance));
        Gizmos.DrawLine(_ledgeCheck.position, _ledgeCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));

        Gizmos.DrawWireSphere(_playerCheck.position + (Vector3)(Vector2.right * entityData.meleeRangeActionDistance * facingDirection), 0.2f);
        Gizmos.DrawWireSphere(_playerCheck.position + (Vector3)(Vector2.right * entityData.minAgroDistance * facingDirection), 0.2f);
        Gizmos.DrawWireSphere(_playerCheck.position + (Vector3)(Vector2.right * entityData.maxAgroDistance * facingDirection), 0.2f);
    }
}
