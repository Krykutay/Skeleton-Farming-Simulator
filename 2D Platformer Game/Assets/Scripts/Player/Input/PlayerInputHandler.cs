using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    PlayerInput _playerInput;
    InputAction _movementAction;
    InputAction _jumpAction;

    public Vector2 rawMovementInput { get; private set; }
    public int normalizedInputX { get; private set; }
    public int normalizedInputY { get; private set;}

    void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _movementAction = _playerInput.actions["Movement"];
        _jumpAction = _playerInput.actions["Jump"];
    }

    void OnEnable()
    {
        //_movementAction.started += OnMoveInput;
        _movementAction.performed += MovementPerform;
        _movementAction.canceled += MovementEnd;

        //_jumpAction.started += OnJumpInput;
        _jumpAction.performed += OnJumpInput;
        //_jumpAction.canceled += OnJumpInput;
    }

    void OnDisable()
    {
        _movementAction.performed -= MovementPerform;
        _movementAction.canceled -= MovementEnd;

        _jumpAction.performed -= OnJumpInput;
    }

    void MovementPerform(InputAction.CallbackContext context)
    {
        rawMovementInput = context.ReadValue<Vector2>();
        normalizedInputX = (int)(rawMovementInput * Vector2.right).normalized.x;
        normalizedInputY = (int)(rawMovementInput * Vector2.up).normalized.y;

    }

    void MovementEnd(InputAction.CallbackContext context)
    {
        rawMovementInput = context.ReadValue<Vector2>();
        normalizedInputX = 0;
        normalizedInputY = 0;
    }


    void OnJumpInput(InputAction.CallbackContext context)
    {
        
    }
}
