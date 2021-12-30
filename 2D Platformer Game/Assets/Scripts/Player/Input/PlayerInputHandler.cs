using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    PlayerInput _playerInput;
    InputAction _upAction;
    InputAction _downAction;
    InputAction _leftAction;
    InputAction _rightAction;
    InputAction _jumpAction;
    InputAction _grabAction;
    InputAction _dashAction;
    InputAction _dashDirectionAction;
    InputAction _primaryAttackAction;
    InputAction _defenseAction;
    InputAction _talkAction;

    public Action talkAction;

    public int xInput { get; private set; }
    public int yInput { get; private set;}
    public bool jumpInput { get; private set; }
    public bool jumpInputStopped { get; private set; }
    public bool grabInput { get; private set; }
    public bool dashInput { get; private set; }
    public bool dashInputStopped { get; private set; }
    public bool attackInput { get; private set; }
    public bool defenseInput { get; private set; }
    public bool talkInput { get; private set; }

    [SerializeField] float _inputHoldTime = 0.2f;

    float _jumpInputStartTime;
    float _dashInputStartTime;

    void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _upAction = _playerInput.actions["Up"];
        _downAction = _playerInput.actions["Down"];
        _leftAction = _playerInput.actions["Left"];
        _rightAction = _playerInput.actions["Right"];
        _jumpAction = _playerInput.actions["Jump"];
        _grabAction = _playerInput.actions["Grab"];
        _dashAction = _playerInput.actions["Dash"];
        _primaryAttackAction = _playerInput.actions["PrimaryAttack"];
        _defenseAction = _playerInput.actions["Parry"];
        _talkAction = _playerInput.actions["Talk"];
    }

    void OnEnable()
    {
        _upAction.performed += UpStart;
        _upAction.canceled += UpCancel;
        _downAction.performed += DownStart;
        _downAction.canceled += DownCancel;
        _leftAction.performed += LeftStart;
        _leftAction.canceled += LeftCancel;
        _rightAction.performed += RightStart;
        _rightAction.canceled += RightCancel;

        _jumpAction.performed += JumpStart;
        _jumpAction.canceled += JumpCancel;

        _grabAction.performed += GrabStart;
        _grabAction.canceled += GrabCancel;

        _dashAction.performed += DashStart;
        _dashAction.canceled += DashCancel;

        _primaryAttackAction.started += PrimaryAttackStart;
        _primaryAttackAction.canceled += PrimaryAttackCancel;

        _defenseAction.started += DefenseStart;
        _defenseAction.canceled += DefenseCancel;

        _talkAction.started += TalkStart;
    }

    void OnDisable()
    {
        _upAction.performed -= UpStart;
        _upAction.canceled -= UpCancel;
        _downAction.performed -= DownStart;
        _downAction.canceled -= DownCancel;
        _leftAction.performed -= LeftStart;
        _leftAction.canceled -= LeftCancel;
        _rightAction.performed -= RightStart;
        _rightAction.canceled -= RightCancel;

        _jumpAction.performed -= JumpStart;
        _jumpAction.canceled -= JumpCancel;

        _grabAction.performed -= GrabStart;
        _grabAction.canceled -= GrabCancel;

        _dashAction.performed -= DashStart;
        _dashAction.canceled -= DashCancel;

        _primaryAttackAction.started -= PrimaryAttackStart;
        _primaryAttackAction.canceled -= PrimaryAttackCancel;

        _defenseAction.started -= DefenseStart;
        _defenseAction.canceled -= DefenseCancel;

        _talkAction.started -= TalkStart;
    }

    void Update()
    {
        CheckJumpInputHoldTime();
        CheckDashInputHoldTime();
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

    public void UseDashInput()
    {
        dashInput = false;
    }

    void CheckDashInputHoldTime()
    {
        if (Time.time >= _dashInputStartTime + _inputHoldTime)
        {
            dashInput = false;
        }
    }

    void UpStart(InputAction.CallbackContext context)
    {
        if (_downAction.inProgress)
            yInput = 0;
        else
            yInput = 1;
    }

    void UpCancel(InputAction.CallbackContext context)
    {
        if (_downAction.inProgress)
            yInput = -1;
        else
            yInput = 0;
    }

    void DownStart(InputAction.CallbackContext context)
    {
        if (_upAction.inProgress)
            yInput = 0;
        else
            yInput = -1;
    }

    void DownCancel(InputAction.CallbackContext context)
    {
        if (_upAction.inProgress)
            yInput = 1;
        else
            yInput = 0;
    }

    void LeftStart(InputAction.CallbackContext context)
    {
        if (_rightAction.inProgress)
            xInput = 0;
        else
            xInput = -1;
    }

    void LeftCancel(InputAction.CallbackContext context)
    {
        if (_rightAction.inProgress)
            xInput = 1;
        else
            xInput = 0;
    }

    void RightStart(InputAction.CallbackContext context)
    {
        if (_leftAction.inProgress)
            xInput = 0;
        else
            xInput = 1;
    }

    void RightCancel(InputAction.CallbackContext context)
    {
        if (_leftAction.inProgress)
            xInput = -1;
        else
            xInput = 0;
    }

    void JumpStart(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.currentState == PlayPauseState.Paused)
            return;

        jumpInput = true;
        jumpInputStopped = false;
        _jumpInputStartTime = Time.time;
    }

    void JumpCancel(InputAction.CallbackContext context)
    {
        jumpInputStopped = true;
    }

    void GrabStart(InputAction.CallbackContext context)
    {
        grabInput = true;
    }

    void GrabCancel(InputAction.CallbackContext context)
    {
        grabInput = false;
    }

    void DashStart(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.currentState == PlayPauseState.Paused)
            return;

        dashInput = true;
        dashInputStopped = false;
        _dashInputStartTime = Time.time;
    }

    void DashCancel(InputAction.CallbackContext context)
    {
        dashInputStopped = true;
    }

    void PrimaryAttackStart(InputAction.CallbackContext context)
    {
        attackInput = true;
    }

    void PrimaryAttackCancel(InputAction.CallbackContext context)
    {
        attackInput = false;
    }

    void DefenseStart(InputAction.CallbackContext context)
    {
        defenseInput = true;
    }

    void DefenseCancel(InputAction.CallbackContext context)
    {
        defenseInput = false;
    }

    void TalkStart(InputAction.CallbackContext context)
    {
        talkInput = true;
        talkAction?.Invoke();
    }
}
