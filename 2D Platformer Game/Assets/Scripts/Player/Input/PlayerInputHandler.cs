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
    public bool jumpInput { get; private set; }
    public bool jumpInputStopped { get; private set; }

    [SerializeField] float _inputHoldTime = 0.2f;

    float _jumpInputStartTime;

    void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _movementAction = _playerInput.actions["Movement"];
        _jumpAction = _playerInput.actions["Jump"];
    }

    void OnEnable()
    {
        _movementAction.started += MovementStart;
        _movementAction.canceled += MovementEnd;

        _jumpAction.started += JumpStart;
        _jumpAction.canceled += JumpEnd;
    }

    void OnDisable()
    {
        _movementAction.started -= MovementStart;
        _movementAction.canceled -= MovementEnd;

        _jumpAction.started -= JumpStart;
        _jumpAction.canceled -= JumpEnd;
    }

    void Update()
    {
        CheckJumpInputHoldTime();
    }

    void MovementStart(InputAction.CallbackContext context)
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


    void JumpStart(InputAction.CallbackContext context)
    {
        jumpInput = true;
        jumpInputStopped = false;
        _jumpInputStartTime = Time.time;
    }

    void JumpEnd(InputAction.CallbackContext context)
    {
        jumpInputStopped = true;
    }

    public void UseJumpInput()
    {
        jumpInput = false;
    }

    void CheckJumpInputHoldTime()
    {
        if (Time.time >= _jumpInputStartTime + _inputHoldTime)
        {
            jumpInput = false;
        }
    }
}
