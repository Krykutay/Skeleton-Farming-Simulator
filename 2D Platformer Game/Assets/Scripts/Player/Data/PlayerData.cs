using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Player Stats")]
    [SerializeField] float _maxHealth = 5f;

    [Header("Move State")]
    [SerializeField] float _movementVelocity = 10f;

    [Header("Defense State")]
    [SerializeField] float _parryMovementVelocity = 5f;

    [Header("Jump State")]
    [SerializeField] float _jumpVelocity = 15f;
    [SerializeField] int _amountOfJumps = 2;

    [Header("Wall Jump State")]
    [SerializeField] float _wallJumpVelocity = 20f;
    [SerializeField] float _wallJumpTime = 0.4f;
    [SerializeField] Vector2 _wallJumpAngle = new Vector2(1, 2);

    [Header("In Air State")]
    [SerializeField] float _coyoteTime = 0.2f;
    [SerializeField] float _variableJumpHeightMultiplier = 0.5f;

    [Header("Wall Slide State")]
    [SerializeField] float _wallSlideVelocity = 2f;

    [Header("Wall Climb State")]
    [SerializeField] float _wallClimbVelocity = 3f;

    [Header("Ledge Climb State")]
    [SerializeField] Vector2 _startOffset;
    [SerializeField] Vector2 _stopOffset;

    [Header("Dash State")]
    [SerializeField] float _dashCooldown = 1f;
    [SerializeField] float _maxHoldTime = 1f;
    [SerializeField] float _holdTimeScale = 0.25f;
    [SerializeField] float _dashTime = 0.2f;
    [SerializeField] float _dashVelocity = 30f;
    [SerializeField] float _drag = 10f;
    [SerializeField] float _dashEndYMultiplier = 0.2f;
    [SerializeField] float _distBetweenAfterImages = 0.5f;
    [SerializeField] float _dashDamage = 8f;

    [Header("Crouch States")]
    [SerializeField] float _crouchMovementVelocity = 5f;
    [SerializeField] float _crouchColliderHeight = 0.8f;
    [SerializeField] float _standColliderHeight = 1.6f;

    [Header("Check Variables")]
    [SerializeField] float _groundCheckRadius = 0.3f;
    [SerializeField] float _wallCheckDistance = 0.5f;
    [SerializeField] LayerMask _ground;

    public float maxHealth { get { return _maxHealth; } }
    public float movementVelocity { get { return _movementVelocity; } }
    public float parryMovementVelocity { get { return _parryMovementVelocity; } }
    public float jumpVelocity { get { return _jumpVelocity; } }
    public int amountOfJumps { get { return _amountOfJumps; } }
    public float wallJumpVelocity { get { return _wallJumpVelocity; } }
    public float wallJumpTime { get { return _wallJumpTime; } }
    public Vector2 wallJumpAngle { get { return _wallJumpAngle; } }
    public float coyoteTime { get { return _coyoteTime; } }
    public float variableJumpHeightMultiplier { get { return _variableJumpHeightMultiplier; } }
    public float wallSlideVelocity { get { return _wallSlideVelocity; } }
    public float wallClimbVelocity { get { return _wallClimbVelocity; } }
    public Vector2 startOffset { get { return _startOffset; } }
    public Vector2 stopOffset { get { return _stopOffset; } }
    public float dashCooldown { get { return _dashCooldown; } }
    public float maxHoldTime { get { return _maxHoldTime; } }
    public float holdTimeScale { get { return _holdTimeScale; } }
    public float dashTime { get { return _dashTime; } }
    public float dashVelocity { get { return _dashVelocity; } }
    public float drag { get { return _drag; } }
    public float dashEndYMultiplier { get { return _dashEndYMultiplier; } }
    public float distBetweenAfterImages { get { return _distBetweenAfterImages; } }
    public float dashDamage { get { return _dashDamage; } }
    public float crouchMovementVelocity { get { return _crouchMovementVelocity; } }
    public float crouchColliderHeight { get { return _crouchColliderHeight; } }
    public float standColliderHeight { get { return _standColliderHeight; } }
    public float groundCheckRadius { get { return _groundCheckRadius; } }
    public float wallCheckDistance { get { return _wallCheckDistance; } }
    public LayerMask ground { get { return _ground; } }

    public void IncreaseMaxHealth()
    {
        if (_maxHealth < 9)
            _maxHealth++;
    }

    public void SetInitialMaxHealth()
    {
        _maxHealth = 5f;
    }
}
