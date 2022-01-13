using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public Action OnPauseAction;

    [SerializeField] PlayerInput _playerInput;

    InputAction _pauseAction;

    void Awake()
    {
        Instance = this;

        _pauseAction = _playerInput.actions["Pause"];
    }

    void OnEnable()
    {
        _pauseAction.started += PauseStart;
    }

    void OnDisable()
    {
        _pauseAction.started -= PauseStart;
    }

    void PauseStart(InputAction.CallbackContext context)
    {
        OnPauseAction?.Invoke();
    }

}
