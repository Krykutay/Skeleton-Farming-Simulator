using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _movementSpeed = 10f;
    [SerializeField] float _jumpForce = 16f;

    Rigidbody2D _rb;

    float _movementInputDirection;

    bool _isFacingRight = true;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckInput();
        CheckMovementDirection();
    }

    void FixedUpdate()
    {
        ApplyMovement();
    }

    void CheckMovementDirection()
    {
        if (_isFacingRight && _movementInputDirection < 0)
            Flip();
        else if (!_isFacingRight && _movementInputDirection > 0)
            Flip();
    }

    void CheckInput()
    {
        _movementInputDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    void Jump()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
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


}
