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
        _movementAction.performed += MovementPerforming;
        _movementAction.canceled += MovementEnd;

        _jumpAction.performed += JumpPerforming;
        _jumpAction.canceled += JumpEnd;
    }

    void OnDisable()
    {
        _movementAction.performed -= MovementPerforming;
        _movementAction.canceled -= MovementEnd;

        _jumpAction.performed -= JumpPerforming;
        _jumpAction.performed -= JumpEnd;
    }

    void MovementPerforming(InputAction.CallbackContext context)
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


    void JumpPerforming(InputAction.CallbackContext context)
    {
        
    }

    void JumpEnd(InputAction.CallbackContext context)
    {

    }
}
