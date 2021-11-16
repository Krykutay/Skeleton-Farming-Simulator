using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _movementSpeed = 10f;
    [SerializeField] float _jumpForce = 16f;
    [SerializeField] int _amountsOfJumps = 2;
    [SerializeField] int _facingDirection = 1;
    [SerializeField] float _jumpTimerSet = 0.15f;
    [SerializeField] float _turnTimerSet = 0.1f;
    [SerializeField] float __wallJumpTimerSet = 0.5f;
    [Header ("Dashing")]
    [SerializeField] float _dashTime;
    [SerializeField] float _dashSpeed;
    [SerializeField] float _distanceBetweenImages;
    [SerializeField] float _dashCooldown;

    [SerializeField] float _groundCheckRadius;
    [SerializeField] float _wallCheckDistance;
    [SerializeField] float _wallSlidingSpeed;
    [SerializeField] float _airDragMultiplier = 0.95f;
    [SerializeField] float _variableJumpHeightMultiplier = 0.5f;
    [SerializeField] float _wallJumpForce;
    [SerializeField] float _ledgeClimbXOffset1 = 0f;
    [SerializeField] float _ledgeClimbYOffset1 = 0f;
    [SerializeField] float _ledgeClimbXOffset2 = 0f;
    [SerializeField] float _ledgeClimbYOffset2 = 0f;

    [SerializeField] Transform _groundCheck;
    [SerializeField] Transform _wallCheck;
    [SerializeField] Transform _ledgeCheck;

    [SerializeField] Vector2 _wallJumpDirection;

    [SerializeField] LayerMask _groundLayer;

    Rigidbody2D _rb;
    Animator _anim;
    Vector2 _ledgePosBottom;
    Vector2 _ledgePos1;
    Vector2 _ledgePos2;

    float _movementInputDirection;
    int _amountsOfJumpsLeft;
    float _jumpTimer;
    float _turnTimer = 0.1f;
    float _wallJumpTimer;
    int _lastWallJumpDirection;
    float _dashTimeLeft;
    float _lastImageXpos;
    float _lastDashTime = -100f;

    bool _isFacingRight = true;
    bool _isWalking;
    bool _isGrounded;
    bool _isTouchingWall;
    bool _isWallSliding;
    bool _canNormalJump;
    bool _canWallJump;
    bool _isAttemptingToJump;
    bool _checkJumpMultiplier;
    bool _canMove;
    bool _canFlip;
    bool _hasWallJumped;
    bool _isTouchingLedge;
    bool _canClimbLedge = false;
    bool _ledgeDetected;
    bool _isDashing;

    ObjectPoolingManager _objectPoolingManagerInstance;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _objectPoolingManagerInstance = ObjectPoolingManager.Instance;
    }

    void Start()
    {
        _amountsOfJumpsLeft = _amountsOfJumps;
        _wallJumpDirection.Normalize();
    }

    void Update()
    {
        CheckInput();
        CheckMovementDirection();
        UpdateAnimations();
        CheckIfCanJump();
        CheckIfWallSliding();
        CheckJump();
        CheckLedgeClimb();
        CheckDash();
    }

    void FixedUpdate()
    {
        ApplyMovement();
        CheckSurroundings();
    }

    void CheckIfWallSliding()
    {
        if (_isTouchingWall && _movementInputDirection == _facingDirection && _rb.velocity.y < 0 && !_canClimbLedge)
        {
            _isWallSliding = true;
        }
        else
        {
            _isWallSliding = false;
        }
    }

    void CheckLedgeClimb()
    {
        if (_ledgeDetected && !_canClimbLedge)
        {
            _canClimbLedge = true;

            if (_isFacingRight)
            {
                _ledgePos1 = new Vector2(
                    Mathf.Floor(_ledgePosBottom.x + _wallCheckDistance) - _ledgeClimbXOffset1,
                    Mathf.Floor(_ledgePosBottom.y) + _ledgeClimbYOffset1);

                _ledgePos2 = new Vector2(
                    Mathf.Floor(_ledgePosBottom.x + _wallCheckDistance) + _ledgeClimbXOffset2,
                    Mathf.Floor(_ledgePosBottom.y) + _ledgeClimbYOffset2);
            }
            else
            {
                _ledgePos1 = new Vector2(
                    Mathf.Ceil(_ledgePosBottom.x - _wallCheckDistance) + _ledgeClimbXOffset1,
                     Mathf.Floor(_ledgePosBottom.y) + _ledgeClimbYOffset1);

                _ledgePos2 = new Vector2(
                    Mathf.Ceil(_ledgePosBottom.x - _wallCheckDistance) - _ledgeClimbXOffset2,
                    Mathf.Floor(_ledgePosBottom.y) + _ledgeClimbYOffset2);
            }

            _canMove = false;
            _canFlip = false;

            _anim.SetBool("canClimbLedge", _canClimbLedge);
        }

        if (_canClimbLedge)
        {
            transform.position = _ledgePos1;
        }

    }

    public void FinishedLedgeClimb()
    {
        _canClimbLedge = false;
        transform.position = _ledgePos2;
        _canMove = true;
        _canFlip = true;
        _ledgeDetected = false;
        _anim.SetBool("canClimbLedge", _canClimbLedge);
    }

    void CheckSurroundings()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);

        _isTouchingWall = Physics2D.Raycast(_wallCheck.position, transform.right, _wallCheckDistance, _groundLayer);

        _isTouchingLedge = Physics2D.Raycast(_ledgeCheck.position, transform.right, _wallCheckDistance, _groundLayer);

        if (_isTouchingWall && !_isTouchingLedge && !_ledgeDetected)
        {
            _ledgeDetected = true;
            _ledgePosBottom = _wallCheck.position;
        }
    }

    void CheckMovementDirection()
    {
        if (_isFacingRight && _movementInputDirection < 0)
            Flip();
        else if (!_isFacingRight && _movementInputDirection > 0)
            Flip();

        if (Mathf.Abs(_rb.velocity.x) > 0.01f)
            _isWalking = true;
        else
            _isWalking = false;
    }

    void UpdateAnimations()
    {
        _anim.SetBool("isWalking", _isWalking);
        _anim.SetBool("isGrounded", _isGrounded);
        _anim.SetFloat("yVelocity", _rb.velocity.y);
        _anim.SetBool("isWallSliding", _isWallSliding);
    }

    void CheckInput()
    {
        _movementInputDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            if (_isGrounded || (_amountsOfJumpsLeft > 0 && !_isTouchingWall))
            {
                NormalJump();
            }
            else
            {
                _jumpTimer = _jumpTimerSet;
                _isAttemptingToJump = true;
            }
        }

        if (Input.GetButtonDown("Horizontal") && _isTouchingWall)
        {
            if (!_isGrounded && _movementInputDirection != _facingDirection)
            {
                _canMove = false;
                _canFlip = false;

                _turnTimer = _turnTimerSet;
            }
        }

        if (_turnTimer > 0)
        {
            _turnTimer -= Time.deltaTime;

            if (_turnTimer <= 0)
            {
                _canMove = true;
                _canFlip = true;
            }
        }

        if (!Input.GetButton("Jump") && _checkJumpMultiplier)
        {
            _checkJumpMultiplier = false;
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * _variableJumpHeightMultiplier);
        }

        if (Input.GetButtonDown("Dash"))
        {
            if (Time.time >= (_lastDashTime + _dashCooldown))
                AttemptToDash();
        }
    }

    void AttemptToDash()
    {
        _isDashing = true;
        _dashTimeLeft = _dashTime;
        _lastDashTime = Time.time;

        GameObject afterImage = _objectPoolingManagerInstance.Get("afterImage");
        afterImage.SetActive(true);
        _lastImageXpos = transform.position.x;
    }

    void CheckDash()
    {
        if (_isDashing)
        {
            if (_dashTimeLeft > 0)
            {
                _canMove = false;
                _canFlip = false;

                _rb.velocity = new Vector2(_dashSpeed * _facingDirection, _rb.velocity.y);  // Set to 0 for Hallow Knight style
                _dashTimeLeft -= Time.deltaTime;

                if (Mathf.Abs(transform.position.x - _lastImageXpos) > _distanceBetweenImages)
                {
                    GameObject afterImage = _objectPoolingManagerInstance.Get("afterImage");
                    afterImage.SetActive(true);
                    _lastImageXpos = transform.position.x;
                }
            }

            if (_dashTimeLeft <= 0 || _isTouchingWall)
            {
                _isDashing = false;
                _canMove = true;
                _canFlip = true;
            }
        }
    }

    void CheckIfCanJump()
    {
        if ((_isGrounded && _rb.velocity.y <= 0.01f))
        {
            _amountsOfJumpsLeft = _amountsOfJumps;
        }

        if (_isTouchingWall)
        {
            _canWallJump = true;
        }

        if (_amountsOfJumpsLeft <= 0)
        {
            _canNormalJump = false;
        }
        else
        {
            _canNormalJump = true;
        }

    }

    void CheckJump()
    {
        if (_jumpTimer > 0)
        {
            // WallJump
            if (!_isGrounded && _isTouchingWall && _movementInputDirection != 0 && _movementInputDirection != _facingDirection)
            {
                WallJump();
            }
            else if (_isGrounded)
            {
                NormalJump();
            }
        }

        if (_isAttemptingToJump)
        {
            _jumpTimer -= Time.deltaTime;
        }

        if (_wallJumpTimer > 0)
        {
            if (_hasWallJumped && _movementInputDirection == -_lastWallJumpDirection)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, 0f);
                _hasWallJumped = false;
            }
            else if(_wallJumpTimer <= 0)
            {
                _hasWallJumped = false;
            }
            else
            {
                _wallJumpTimer -= Time.deltaTime;
            }
        }
    }

    void NormalJump()
    {
        if (_canNormalJump && !_isWallSliding)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
            _amountsOfJumpsLeft--;
            _jumpTimer = 0;
            _isAttemptingToJump = false;
            _checkJumpMultiplier = true;
        }
    }

    void WallJump()
    {
        if (_canWallJump)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, 0f);
            _isWallSliding = false;
            _amountsOfJumpsLeft = _amountsOfJumps;
            _amountsOfJumpsLeft--;
            Vector2 forceToAdd = new Vector2(_wallJumpForce * _wallJumpDirection.x * _movementInputDirection, _wallJumpForce * _wallJumpDirection.y);
            _rb.AddForce(forceToAdd, ForceMode2D.Impulse);
            _jumpTimer = 0;
            _isAttemptingToJump = false;
            _checkJumpMultiplier = true;
            _turnTimer = 0;
            _canMove = true;
            _canFlip = true;
            _hasWallJumped = true;
            _wallJumpTimer = __wallJumpTimerSet;
            _lastWallJumpDirection = -_facingDirection;
        }
    }

    void ApplyMovement()
    {
        if (!_isGrounded && !_isWallSliding && _movementInputDirection == 0)
        {
            _rb.velocity = new Vector2(_rb.velocity.x * _airDragMultiplier, _rb.velocity.y);
        }
        else if (_canMove)
        {
            _rb.velocity = new Vector2(_movementSpeed * _movementInputDirection, _rb.velocity.y);
        }

        if (_isWallSliding)
        {
            if (_rb.velocity.y < -_wallSlidingSpeed)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, -_wallSlidingSpeed);
            }
        }
    }

    void Flip()
    {
        if (!_isWallSliding && _canFlip)
        {
            _facingDirection *= -1;
            _isFacingRight = !_isFacingRight;
            transform.Rotate(0f, 180f, 0f);
        }

    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_groundCheck.position, _groundCheckRadius);

        Gizmos.DrawLine(_wallCheck.position, new Vector3(_wallCheck.position.x + _wallCheckDistance, _wallCheck.position.y, _wallCheck.position.z));

        //Gizmos.DrawLine(_ledgePos1, _ledgePos2);
    }

}
