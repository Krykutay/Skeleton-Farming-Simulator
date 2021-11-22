using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    [SerializeField] float _movementVelocity = 10f;

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


    [Header("Check Variables")]
    [SerializeField] float _groundCheckRadius = 0.3f;
    [SerializeField] float _wallCheckDistance = 0.5f;
    [SerializeField] LayerMask _ground;

    public float movementVelocity { get { return _movementVelocity; } }
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
    public float groundCheckRadius { get { return _groundCheckRadius; } }
    public float wallCheckDistance { get { return _wallCheckDistance; } }
    public LayerMask ground { get { return _ground; } }
}
