using System;
using UnityEngine;

public class Entity : MonoBehaviour, IDamageable
{
    public static Action<Entity> OnEnemyDied;

    public FiniteStateMachine stateMachine;

    public D_Entity entityData;

    public int facingDirection { get; private set; }
    public int lastDamageDirection { get; private set; }
    public int deathCount { get; private set; }

    public Transform playerTransform { get; private set; }

    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }
    public AnimationToStateMachine atsm { get; private set; }

    public Vector3 initialPosition { get; private set; }
    public Quaternion initialRotation { get; private set; }

    protected bool isStunned;
    protected bool isDead;

    [SerializeField] Transform _groundCheck;
    [SerializeField] Transform _wallCheck;
    [SerializeField] Transform _ledgeCheck;
    [SerializeField] Transform _playerCheck;

    EnemyHealthBar _healthbar;

    Transform _hpCanvas;
    Vector2 _velocityWorkspace;

    float _currentHealth;
    float _currentStunResistance;
    float _lastDamagetime;

    public virtual void Awake()
    {
        playerTransform = Player.Instance.transform.Find("Core").Find("PlayerHitPosition").transform;
        _hpCanvas = transform.Find("Canvas");
        _healthbar = _hpCanvas.Find("HealthBar").GetComponent<EnemyHealthBar>();

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        atsm = GetComponent<AnimationToStateMachine>();

        stateMachine = new FiniteStateMachine();
    }

    public virtual void OnEnable()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        _healthbar.gameObject.SetActive(true);
        _currentHealth = entityData.maxHealth;
        _currentStunResistance = entityData.stunResistance;
        facingDirection = 1;
        deathCount = 0;
        isStunned = false;
        isDead = false;

        _healthbar.SetMaxHealth(entityData.maxHealth);
        _healthbar.SetCurrentHealth(entityData.maxHealth, 0);

        PowerupManager.Instance.OnVaporize += PowerupManager_Vaporize;
    }

    void OnDisable()
    {
        PowerupManager.Instance.OnVaporize -= PowerupManager_Vaporize;
    }

    public virtual void Start()
    {
        
    }

    public virtual void Update()
    {
        if (GameManager.Instance.currentState == PlayPauseState.Paused)
            return;

        stateMachine.currentState.LogicUpdate();

        //anim.SetFloat("yVelocity", rb.velocity.y);

        if (Time.time >= _lastDamagetime + entityData.stunRecoveryTime)
        {
            ResetStunResistance();
        }
    }

    public virtual void FixedUpdate()
    {
        if (GameManager.Instance.currentState == PlayPauseState.Paused)
            return;

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
            return false;

        float damageAmount = attackDetails.damageAmount;
        if (PowerupManager.Instance.isDamagePowerupActive)
            damageAmount *= 1.5f;

        _lastDamagetime = Time.time;

        if (attackDetails.position.x > transform.position.x)
            lastDamageDirection = -1;
        else
            lastDamageDirection = 1;

        DamageHop(entityData.damageHopSpeed);

        _currentHealth -= damageAmount;
        _healthbar.SetCurrentHealth((int)_currentHealth, (int)damageAmount);
        _currentStunResistance -= attackDetails.stunDamageAmount;

        DamagePopup damagePopup = DamagePopupPool.Instance.Get(transform.position, Quaternion.identity);

        if (attackDetails.stunDamageAmount > 0)
            damagePopup.Setup((int)damageAmount, true, lastDamageDirection);
        else
            damagePopup.Setup((int)damageAmount, false, lastDamageDirection);

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

    public virtual void DamageBySurface()
    {
        if (isDead)
            return;

        _currentHealth -= 10;
        _healthbar.SetCurrentHealth((int)_currentHealth, 10);

        DamagePopup damagePopup = DamagePopupPool.Instance.Get(transform.position, Quaternion.identity);

        damagePopup.Setup(10, false, facingDirection);

        if (_currentHealth <= 0f)
        {
            isDead = true;
        }
    }

    public virtual void PowerupManager_Vaporize()
    {
        if (deathCount == 1)
            deathCount = 2;

        DropLootOnDeath();
        VaporizeParticle1Pool.Instance.Get(transform.position, Quaternion.identity);
        OnEnemyDied?.Invoke(this);
        ScoreManager.Instance.Enemy_Killed();

        SoundManager.Instance.Stop(SoundManager.SoundTags.SkeletonRespawn);
        anim.WriteDefaultValues();
    }

    public virtual void JustDied()
    {
        deathCount++;
        if (deathCount == 2)
            ScoreManager.Instance.Enemy_Killed();

        isDead = true;
        SoundManager.Instance.Play(SoundManager.SoundTags.SkeletonDie);
        _healthbar.gameObject.SetActive(false);
        DropLootOnDeath();
    }

    public virtual void StunnedByPlayerParry(int parryDirection)
    {
        lastDamageDirection = parryDirection;
    }

    public virtual void DamageHop(float velocity)
    {
        _velocityWorkspace.Set(rb.velocity.x, velocity);
        rb.velocity = _velocityWorkspace;
    }

    void DropLootOnDeath()
    {
        int amountOfLoots;

        if (deathCount == 0)
            amountOfLoots = UnityEngine.Random.Range(3, 6);
        else if (deathCount == 1)
            amountOfLoots = UnityEngine.Random.Range(1, 3);
        else
            amountOfLoots = UnityEngine.Random.Range(2, 4);


        for (int i = 0; i < amountOfLoots; i++)
        {
            int lootColor = UnityEngine.Random.Range(0, 6);
            if (lootColor == 0)
                DropLootRedPool.Instance.Get(new Vector3(transform.position.x + UnityEngine.Random.Range(-0.6f, 0.6f), transform.position.y + UnityEngine.Random.Range(-0.6f, 0.6f)), Quaternion.identity);
            else if (lootColor == 1)
                DropLootYellowPool.Instance.Get(new Vector3(transform.position.x + UnityEngine.Random.Range(-0.6f, 0.6f), transform.position.y + UnityEngine.Random.Range(-0.6f, 0.6f)), Quaternion.identity);
            else if (lootColor == 2)
                DropLootGreenPool.Instance.Get(new Vector3(transform.position.x + UnityEngine.Random.Range(-0.6f, 0.6f), transform.position.y + UnityEngine.Random.Range(-0.6f, 0.6f)), Quaternion.identity);
            else if (lootColor == 3)
                DropLootCyanPool.Instance.Get(new Vector3(transform.position.x + UnityEngine.Random.Range(-0.6f, 0.6f), transform.position.y + UnityEngine.Random.Range(-0.6f, 0.6f)), Quaternion.identity);
            else if (lootColor == 4)
                DropLootBluePool.Instance.Get(new Vector3(transform.position.x + UnityEngine.Random.Range(-0.6f, 0.6f), transform.position.y + UnityEngine.Random.Range(-0.6f, 0.6f)), Quaternion.identity);
            else
                DropLootPurplePool.Instance.Get(new Vector3(transform.position.x + UnityEngine.Random.Range(-0.6f, 0.6f), transform.position.y + UnityEngine.Random.Range(-0.6f, 0.6f)), Quaternion.identity);
        }
    }

    public virtual void Flip()
    {
        facingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
        _hpCanvas.Rotate(0f, 180f, 0f);
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
        _healthbar.gameObject.SetActive(true);
        _healthbar.SetMaxHealth(entityData.maxHealth);
        _healthbar.SetCurrentHealth(entityData.maxHealth, 0);
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
