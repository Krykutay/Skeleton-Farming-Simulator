using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _movementSpeed = 10f;
    [SerializeField] float _jumpForce = 16f;

    [SerializeField] Transform _groundCheck;
    [SerializeField] float _groundCheckRadius;
    [SerializeField] LayerMask _groundLayer;

    [SerializeField] int _amountsOfJumps = 2;

    Rigidbody2D _rb;
    Animator _anim;

    float _movementInputDirection;
    int _amountsOfJumpsLeft;

    bool _isFacingRight = true;
    bool _isWalking;
    bool _isGrounded;
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
    }

    void FixedUpdate()
    {
        ApplyMovement();
        CheckSurroundings();
    }

    void CheckSurroundings()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);
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
    }

    void CheckInput()
    {
        _movementInputDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
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
        _rb.velocity = new Vector2(_movementSpeed * _movementInputDirection, _rb.velocity.y);
    }

    void Flip()
    {
        _isFacingRight = !_isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_groundCheck.position, _groundCheckRadius);
    }

}
