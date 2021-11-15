using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _movementSpeed = 10f;
    [SerializeField] float _jumpForce = 16f;

    [SerializeField] Transform _groundCheck;
    [SerializeField] Transform _wallCheck;
    [SerializeField] float _groundCheckRadius;
    [SerializeField] float _wallCheckDistance;
    [SerializeField] float _wallSlidingSpeed;
    [SerializeField] float _movementForceInAir;
    [SerializeField] float _airDragMultiplier = 0.95f;
    [SerializeField] float _variableJumpHeightMultiplier = 0.5f;

    [SerializeField] LayerMask _groundLayer;

    [SerializeField] int _amountsOfJumps = 2;

    Rigidbody2D _rb;
    Animator _anim;

    float _movementInputDirection;
    int _amountsOfJumpsLeft;

    bool _isFacingRight = true;
    bool _isWalking;
    bool _isGrounded;
    bool _isTouchingWall;
    bool _isWallSliding;
    bool _canJump;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    void Start()
    {
        _amountsOfJumpsLeft = _amountsOfJumps;
    }

    void Update()
    {
        CheckInput();
        CheckMovementDirection();
        UpdateAnimations();
        CheckIfCanJump();
        CheckIfWallSliding();
    }

    void FixedUpdate()
    {
        ApplyMovement();
        CheckSurroundings();
    }

    void CheckIfWallSliding()
    {
        if (_isTouchingWall && !_isGrounded && _rb.velocity.y < 0)
        {
            _isWallSliding = true;
        }
        else
        {
            _isWallSliding = false;
        }
    }

    void CheckSurroundings()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);

        _isTouchingWall = Physics2D.Raycast(_wallCheck.position, transform.right, _wallCheckDistance, _groundLayer);
    }

    void CheckMovementDirection()
    {
        if (_isFacingRight && _movementInputDirection < 0)
            Flip();
        else if (!_isFacingRight && _movementInputDirection > 0)
            Flip();

        if (_rb.velocity.x != 0)
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
            Jump();
        }

        if (Input.GetButtonUp("Jump"))
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * _variableJumpHeightMultiplier);
        }
    }

    void CheckIfCanJump()
    {
        if (_isGrounded && _rb.velocity.y <= 0.1f)
        {
            _amountsOfJumpsLeft = _amountsOfJumps;
        }

        if (_amountsOfJumpsLeft <= 0)
            _canJump = false;
        else
            _canJump = true;

    }

    void Jump()
    {
        if (_canJump)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
            _amountsOfJumpsLeft--;
        }
    }

    void ApplyMovement()
    {
        if (_isGrounded)
        {
            _rb.velocity = new Vector2(_movementSpeed * _movementInputDirection, _rb.velocity.y);
        }
        else if (!_isGrounded && !_isWallSliding && _movementInputDirection != 0)
        {
            Vector2 forceToAdd = new Vector2(_movementForceInAir * _movementInputDirection, 0);
            _rb.AddForce(forceToAdd);

            if (Mathf.Abs(_rb.velocity.x) > _movementSpeed)
            {
                _rb.velocity = new Vector2(_movementSpeed * _movementInputDirection, _rb.velocity.y);
            }
        }
        else if (!_isGrounded && !_isWallSliding && _movementInputDirection == 0)
        {
            _rb.velocity = new Vector2(_rb.velocity.x * _airDragMultiplier, _rb.velocity.y);
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
        if (!_isWallSliding)
        {
            _isFacingRight = !_isFacingRight;
            transform.Rotate(0f, 180f, 0f);
        }

    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_groundCheck.position, _groundCheckRadius);

        Gizmos.DrawLine(_wallCheck.position, new Vector3(_wallCheck.position.x + _wallCheckDistance, _wallCheck.position.y, _wallCheck.position.z));
    }

}
