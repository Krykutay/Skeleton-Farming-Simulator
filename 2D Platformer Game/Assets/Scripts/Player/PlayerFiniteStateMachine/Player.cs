using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, IShopCustomer
{
    public Action OnPlayerDied;
    public static Player Instance { get; private set; }

    [Header("Data")]
    [SerializeField] PlayerData _playerData;
    [SerializeField] WeaponData _weaponData;

    [Header("Checks")]
    public Transform _groundCheck;
    [SerializeField] Transform _wallCheck;
    [SerializeField] Transform _verticalLedgeCheck;
    [SerializeField] Transform _ceilingCheck;
    [SerializeField] Transform _playerHitPosition;
    [SerializeField] Transform _attackPosition;

    [Header("BodyPartsToChange")]
    [SerializeField] SpriteRenderer _chestSpriteRenderer;
    [SerializeField] SpriteRenderer _headSpriteRenderer;
    [SerializeField] SpriteRenderer _rightSwordSpriteRenderer;
    [SerializeField] SpriteRenderer _leftSwordSpriteRenderer;
    [SerializeField] TrailRenderer _rightSwordTrailRenderer;
    [SerializeField] TrailRenderer _leftSwordTrailRenderer;

    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerInAirState inAirState { get; private set; }
    public PlayerLandState landState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerWallGrabState wallGrabState { get; private set; }
    public PlayerWallClimbState wallClimbState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerLedgeClimbState ledgeClimbState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerCrouchIdleState crouchIdleState { get; private set; }
    public PlayerCrouchMoveState crouchMoveState { get; private set; }
    public PlayerAttackState primaryAttackState { get; private set; }
    public PlayerDefenseState defenseState { get; private set; }
    public PlayerDefenseMoveState defenseMoveState { get; private set; }
    public PlayerKnockbackState knockbackState { get; private set; }

    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public BoxCollider2D movementCollider { get; private set; }
    public PlayerInputHandler inputHandler { get; private set; }
    public Transform dashDirectionIndicator { get; private set; }
    public Camera cam { get; private set; }

    public Vector2 currentVelocity { get; private set; }
    public int facingDirection { get; private set; }
    public float initialGravity { get; private set; }
    public float currentHealth { get { return _currentHealth; } }
    public float maxHealth { get { return _playerData.maxHealth; } }

    PlayerHealth _playerHealth;
    PlayerInventory _playerInventory;

    Animator _bodyAnim;
    IEnumerator _hurt;

    float _knockbackStrength;
    Vector2 _knockbackAngle;
    int _knockbackDirection;

    Vector2 _workSpace;
    float _currentHealth;
    float _initialHitPositionX;
    float _initialHitPositionY;
    bool _knockbacked;
    AttackDetails _attackDetails;
    int _randInt;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.LogError("There can not be two player instances at once");

        anim = GetComponent<Animator>();
        inputHandler = GetComponent<PlayerInputHandler>();
        rb = GetComponent<Rigidbody2D>();
        movementCollider = GetComponent<BoxCollider2D>();
        dashDirectionIndicator = transform.Find("DashDirectionIndicator");
        _bodyAnim = transform.Find("BodyParts").GetComponent<Animator>();
        _playerHealth = GetComponent<PlayerHealth>();
        _playerInventory = GetComponent<PlayerInventory>();
        cam = Camera.main;

        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, _playerData, "idle");
        moveState = new PlayerMoveState(this, stateMachine, _playerData, "move");
        jumpState = new PlayerJumpState(this, stateMachine, _playerData, "inAir");
        inAirState = new PlayerInAirState(this, stateMachine, _playerData, "inAir");
        landState = new PlayerLandState(this, stateMachine, _playerData, "land");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, _playerData, "wallSlide");
        wallGrabState = new PlayerWallGrabState(this, stateMachine, _playerData, "wallGrab");
        wallClimbState = new PlayerWallClimbState(this, stateMachine, _playerData, "wallClimb");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, _playerData, "inAir");
        ledgeClimbState = new PlayerLedgeClimbState(this, stateMachine, _playerData, "ledgeClimbState");
        dashState = new PlayerDashState(this, stateMachine, _playerData, "inAir");
        crouchIdleState = new PlayerCrouchIdleState(this, stateMachine, _playerData, "crouchIdle");
        crouchMoveState = new PlayerCrouchMoveState(this, stateMachine, _playerData, "crouchMove");
        primaryAttackState = new PlayerAttackState(this, stateMachine, _playerData, "attack", _weaponData, _attackPosition);
        defenseState = new PlayerDefenseState(this, stateMachine, _playerData, "parry");
        defenseMoveState = new PlayerDefenseMoveState(this, stateMachine, _playerData, "parryMove");
        knockbackState = new PlayerKnockbackState(this, stateMachine, _playerData, "inAir");
    }

    void OnEnable()
    {
        stateMachine.Initialize(idleState);
        facingDirection = 1;
        transform.rotation = Quaternion.identity;
        _currentHealth = _playerData.maxHealth;
        _playerHealth.SetHealthIndicatorColor();
        initialGravity = rb.gravityScale;
    }

    void Start()
    {
        _initialHitPositionX = _playerHitPosition.localPosition.x;
        _initialHitPositionY = _playerHitPosition.localPosition.y;

        ActivateOutfit((Items.ItemType)_playerInventory.EquippedOutfit);
        ActivateSwords((Items.ItemType)_playerInventory.EquippedSwords);

        Load_Health();
        Load_Damage();
    }

    void Update()
    {
        if (GameManager.Instance.currentState == PlayPauseState.Paused)
            return;
        
        currentVelocity = rb.velocity;
        stateMachine.currentState.LogicUpdate();
    }

    void LateUpdate()
    {
        if (GameManager.Instance.currentState == PlayPauseState.Paused)
            return;

        if (_knockbacked)
        {
            stateMachine.ChangeState(knockbackState);
            SetVelocity(_knockbackStrength, _knockbackAngle, _knockbackDirection);
        }
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.currentState == PlayPauseState.Paused)
            return;

        stateMachine.currentState.PhysicsUpdate();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (stateMachine.currentState != dashState)
            return;

        _attackDetails.damageAmount = _playerData.dashDamage;
        _attackDetails.position = transform.position;

        if (collision.TryGetComponent<IDamageable>(out var damageable))
        {
            bool isHit = damageable.Damage(_attackDetails);

            if (isHit)
                SoundManager.Instance.Play(SoundManager.SoundTags.SkeletonHurt);
        }
    }  

    public void SetVelocityZero()
    {
        rb.velocity = Vector2.zero;
        currentVelocity = Vector2.zero;
    }

    public void SetVelocityX(float velocity)
    {
        _workSpace.Set(velocity, currentVelocity.y);
        rb.velocity = _workSpace;
        currentVelocity = _workSpace;
    }

    public void SetVelocityY(float velocity)
    {
        _workSpace.Set(currentVelocity.x, velocity);
        rb.velocity = _workSpace;
        currentVelocity = _workSpace;
    }

    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        _workSpace.Set(angle.x * velocity * direction, angle.y * velocity);
        rb.velocity = _workSpace;
        currentVelocity = _workSpace;
    }

    public void SetVelocity(float velocity, Vector2 direction)
    {
        _workSpace = direction * velocity;
        rb.velocity = _workSpace;
        currentVelocity = _workSpace;
    }

    public void SetPlayerHitPositionInCrouch()
    {
        _playerHitPosition.localPosition = new Vector3(_initialHitPositionX, _playerHitPosition.localPosition.y * 2, 0f);
    }

    public void ResetPlayerHitPosition()
    {
        _playerHitPosition.localPosition = new Vector3(_initialHitPositionX, _initialHitPositionY, 0f);
    }

    public void SetPlayerHitPositionOnLedge()
    {
        _playerHitPosition.localPosition = new Vector3(
            _playerHitPosition.localPosition.x + _playerData.startOffset.x,
            _initialHitPositionY,
            0f);
    }

    public void SetKnockbackOver()
    {
        _knockbacked = false;
    }

    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != facingDirection)
        {
            Flip();
        }
    }

    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, _playerData.groundCheckRadius, _playerData.ground);
    }

    public bool CheckIfTouchingWall()
    {
        return Physics2D.Raycast(_wallCheck.position, Vector2.right * facingDirection, _playerData.wallCheckDistance, _playerData.ground);
    }

    public bool CheckIfTouchingWallBack()
    {
        return Physics2D.Raycast(_wallCheck.position, Vector2.right * -facingDirection, _playerData.wallCheckDistance, _playerData.ground);
    }

    public bool CheckIfTouchingVerticalLedge()
    {
        return Physics2D.Raycast(_verticalLedgeCheck.position, Vector2.right * facingDirection, _playerData.wallCheckDistance, _playerData.ground);
    }

    public bool CheckForCeiling()
    {
        return Physics2D.OverlapCircle(_ceilingCheck.position, _playerData.groundCheckRadius, _playerData.ground);
    }

    void Flip()
    {
        facingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
    }

    public Vector2 DetermineCornerPosition()
    {
        RaycastHit2D xHit = Physics2D.Raycast(
            _wallCheck.position,
            Vector2.right * facingDirection, _playerData.wallCheckDistance,
            _playerData.ground);
        float xDist = xHit.distance;
        _workSpace.Set((xDist + 0.015f) * facingDirection, 0f);

        RaycastHit2D yHit = Physics2D.Raycast(
            _verticalLedgeCheck.position + (Vector3)_workSpace,
            Vector2.down, _verticalLedgeCheck.position.y - _wallCheck.position.y + 0.015f,
            _playerData.ground);
        float yDist = yHit.distance;
        _workSpace.Set(_wallCheck.position.x + xDist * facingDirection, _verticalLedgeCheck.position.y - yDist);

        return _workSpace;
    }

    public void SetColliderHeight(float height)
    {
        Vector2 center = movementCollider.offset;
        _workSpace.Set(movementCollider.size.x, height);

        center.y += (height - movementCollider.size.y) / 2; 

        movementCollider.size = _workSpace;
        movementCollider.offset = center;
    }

    public void Knockback(AttackDetails attackDetails)
    {
        int enemyDirection;

        if (attackDetails.position.x < _playerHitPosition.position.x)
        {
            enemyDirection = 1;
        }
        else
        {
            enemyDirection = -1;
        }

        _knockbackStrength = attackDetails.knockbackStrength;
        _knockbackAngle = attackDetails.knockbackAngle;
        _knockbackDirection = enemyDirection;

        _knockbacked = true;
    }

    public bool Damage(AttackDetails attackDetails, Entity entity, bool isMeleeHit)
    {
        if (stateMachine.currentState == dashState)
            return false;

        DecreaseHealth(attackDetails, entity, isMeleeHit);

        return true;
    }

    void DecreaseHealth(AttackDetails attackDetails, Entity entity, bool isMeleeHit)
    {
        int enemyDirection;

        if (attackDetails.position.x < _playerHitPosition.position.x)
            enemyDirection = 1;
        else
            enemyDirection = -1;

        if ((stateMachine.currentState == defenseState || stateMachine.currentState == defenseMoveState) && enemyDirection == -facingDirection)
        {
            if (Time.time - stateMachine.currentState.parryStartTime < 0.3f)
            {
                if (isMeleeHit)
                    entity.StunnedByPlayerParry(-enemyDirection);

                SoundManager.Instance.Play(SoundManager.SoundTags.PlayerParry);
                return;
            }

            if (PowerupManager.Instance.isShieldPowerupActive)
            {
                SoundManager.Instance.Play(SoundManager.SoundTags.Shield);
                return;
            }

            SoundManager.Instance.Play(SoundManager.SoundTags.PlayerParry);
            _currentHealth -= attackDetails.damageAmount * 0.5f;
            _playerHealth.SetHealthIndicatorColor();
            if (_hurt != null)
                StopCoroutine(_hurt);
            _hurt = Hurt();
            StartCoroutine(_hurt);
        }
        else
        {
            if (PowerupManager.Instance.isShieldPowerupActive)
            {
                SoundManager.Instance.Play(SoundManager.SoundTags.Shield);
                return;
            }

            _randInt = UnityEngine.Random.Range(0, 2);
            if (_randInt == 0)
                SoundManager.Instance.Play(SoundManager.SoundTags.PlayerHurt1);
            else
                SoundManager.Instance.Play(SoundManager.SoundTags.PlayerHurt2);

            _currentHealth -= attackDetails.damageAmount;
            _playerHealth.SetHealthIndicatorColor();

            if (_hurt != null)
                StopCoroutine(_hurt);
            _hurt = Hurt();
            StartCoroutine(_hurt);
        }

        if (_currentHealth <= 0f)
        {
            Die();
        }
    }

    public void DamageBySurface()
    {
        if (stateMachine.currentState == dashState || _currentHealth <= 0f)
            return;

        if (PowerupManager.Instance.isShieldPowerupActive)
        {
            SoundManager.Instance.Play(SoundManager.SoundTags.Shield);
            return;
        }

        _randInt = UnityEngine.Random.Range(0, 2);
        if (_randInt == 0)
            SoundManager.Instance.Play(SoundManager.SoundTags.PlayerHurt1);
        else
            SoundManager.Instance.Play(SoundManager.SoundTags.PlayerHurt2);

        _currentHealth -= 1;
        _playerHealth.SetHealthIndicatorColor();
        if (_hurt != null)
            StopCoroutine(_hurt);
        _hurt = Hurt();
        StartCoroutine(_hurt);

        if (_currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        DeathChunkParticlePool.Instance.Get(transform.position, Quaternion.Euler(0f, 0f, 0f));
        DeathBloodParticlePool.Instance.Get(transform.position, Quaternion.Euler(0f, 0f, 0f));

        anim.WriteDefaultValues();
        _bodyAnim.WriteDefaultValues();

        OnPlayerDied?.Invoke();
    }

    public void SetCurrentHealth()
    {
        if (_currentHealth < maxHealth)
        {
            _currentHealth++;
            if (_currentHealth > maxHealth)
                _currentHealth = maxHealth;

            _playerHealth.SetHealthIndicatorColor();
        }
    }

    void PlayHurt()
    {
        if (_currentHealth <= 0f)
            return;

        if (_hurt != null)
            StopCoroutine(_hurt);
        _hurt = Hurt();
        StartCoroutine(_hurt);
    }

    IEnumerator Hurt()
    {
        _bodyAnim.SetBool("hurt", true);
        yield return new WaitForSeconds(0.5f);
        _bodyAnim.SetBool("hurt", false);
    }

    void AnimationTrigger()
    {
        stateMachine.currentState.AnimationTrigger();
    }

    void AnimationFinishTrigger()
    {
        stateMachine.currentState.AnimationFinishTrigger();
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(_wallCheck.position, new Vector3(_wallCheck.position.x + _playerData.wallCheckDistance, _wallCheck.position.y, _wallCheck.position.z));
        Gizmos.DrawLine(_verticalLedgeCheck.position, new Vector3(_verticalLedgeCheck.position.x + _playerData.wallCheckDistance, _verticalLedgeCheck.position.y, _verticalLedgeCheck.position.z));

        Gizmos.DrawWireSphere(_ceilingCheck.position, _playerData.groundCheckRadius);

        if (_attackPosition != null)
        {
            Gizmos.DrawWireSphere(_attackPosition.position, _weaponData.attackDetails[0].attackRadius);
        }
    }

    public void BoughtItem(Items.ItemType itemType)
    {
        _playerInventory.AddItem((int)itemType);

        switch (itemType)
        {
            case Items.ItemType.DefaultOutfit:  ActivateOutfit(itemType);   break;
            case Items.ItemType.BlueOutfit:     ActivateOutfit(itemType);   break;
            case Items.ItemType.GreenOutfit:    ActivateOutfit(itemType);   break;
            case Items.ItemType.YellowOutfit:   ActivateOutfit(itemType);   break;
            case Items.ItemType.BrownOutfit:    ActivateOutfit(itemType);   break;
            case Items.ItemType.DefaultSword:   ActivateSwords(itemType);   break;
            case Items.ItemType.BlueSword:      ActivateSwords(itemType);   break;
            case Items.ItemType.CyanSword:      ActivateSwords(itemType);   break;
            case Items.ItemType.GreenSword:     ActivateSwords(itemType);   break;
            case Items.ItemType.RedSword:       ActivateSwords(itemType);   break;
            case Items.ItemType.PurpleSword:    ActivateSwords(itemType);   break;
            case Items.ItemType.DefenseBoost:   ActivateDefenseBoost();     break;
            case Items.ItemType.OffenseBoost:   ActivateOffenseBoost();     break;
        }
    }

    public bool TrySpendTokenAmount(int spendTokenAmount)
    {
        if (ScoreManager.Instance.tokenScore >= spendTokenAmount)
        {
            ScoreManager.Instance.Token_Spent(spendTokenAmount);
            return true;
        }
        else
        {
            return false;
        }
    }

    void ActivateOutfit(Items.ItemType itemType)
    {
        _playerInventory.UpdateEquippedOutfit((int)itemType);

        _chestSpriteRenderer.sprite = Items.GetSprite(itemType)[0];
        _headSpriteRenderer.sprite = Items.GetSprite(itemType)[1];

        _playerHealth.ChangePlayerHealthImage(Items.GetSprite(itemType)[1]);
    }

    void ActivateSwords(Items.ItemType itemType)
    {
        _playerInventory.UpdateEquippedSword((int)itemType);

        _rightSwordSpriteRenderer.sprite = Items.GetSprite(itemType)[0];
        _leftSwordSpriteRenderer.sprite = Items.GetSprite(itemType)[0];

        _rightSwordTrailRenderer.startColor = Items.GetTrailColor(itemType)[0];
        _rightSwordTrailRenderer.endColor = Items.GetTrailColor(itemType)[1];
        _leftSwordTrailRenderer.startColor = Items.GetTrailColor(itemType)[0];
        _leftSwordTrailRenderer.endColor = Items.GetTrailColor(itemType)[1];
    }

    void ActivateDefenseBoost()
    {
        _playerInventory.IncreaseDefensiveBoostCount();

        _playerData.IncreaseMaxHealth();
        _currentHealth = maxHealth;

        _playerHealth.EnableHealthIndicators();
        _playerHealth.SetHealthIndicatorColor();
    }

    void ActivateOffenseBoost()
    {
        _playerInventory.IncreaseOffensiveBoostCount();

        _weaponData.IncreaseDamage();
    }

    void Load_Health()
    {
        if (maxHealth > _playerData.initialMaxHealth && _playerInventory.defensiveBoostCount == 0)
            _playerData.SetInitialMaxHealth();

        if (maxHealth < _playerData.initialMaxHealth + _playerInventory.defensiveBoostCount * _playerData.healthIncreaseAmount)
        {
            _playerData.SetInitialMaxHealth();
            for (int i = 0; i < _playerInventory.defensiveBoostCount; i++)
            {
                _playerData.IncreaseMaxHealth();
                _currentHealth = maxHealth;
            }
        }

        _playerHealth.EnableHealthIndicators();
        _playerHealth.SetHealthIndicatorColor();
        _currentHealth = maxHealth;
    }

    void Load_Damage()
    {
        if (_weaponData.attackDetails[0].damageAmount > _weaponData.initialAttackDamages[0] && _playerInventory.offensiveBoostCount == 0)
            _weaponData.SetInitialDamage();

        if (_weaponData.attackDetails[0].damageAmount < _weaponData.initialAttackDamages[0] + _playerInventory.offensiveBoostCount * _weaponData.damageIncreaseAmount)
        {
            _weaponData.SetInitialDamage();
            for (int i = 0; i < _playerInventory.offensiveBoostCount; i++)
            {
                _weaponData.IncreaseDamage();
            }
        }
    }
}
