using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public enum PlayPauseState
{
    Playing,
    Paused,
}

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject _menu;
    //[SerializeField] GameObject _gameoverPanel;
    [SerializeField] GameObject _player;

    [SerializeField] Transform _respawnPoint;

    [SerializeField] float _respawnTime;

    float _respawnTimeStart;

    bool _respawn;

    CinemachineVirtualCamera _cvc;
    PlayPauseState _currentState;

    void Awake()
    {
        _cvc = transform.parent.Find("Cameras").Find("Player Camera").GetComponent<CinemachineVirtualCamera>();

        _currentState = PlayPauseState.Playing;
    }

    void OnEnable()
    {
        Player.Instance.PlayerDied += Player_PlayerDied;
    }

    void OnDisable()
    {
        Player.Instance.PlayerDied += Player_PlayerDied;
    }

    void Update()
    {
        CheckRespawn();

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Game_Paused();
        }
    }

    void Player_PlayerDied()
    {
        Respawn();
    }

    void Respawn()
    {
        _respawnTimeStart = Time.time;
        _respawn = true;
    }

    void CheckRespawn()
    {
        if (_respawn && Time.time >= _respawnTimeStart + _respawnTime)
        {
            var tempPlayer = Instantiate(_player, _respawnPoint.position, Quaternion.Euler(0f, 0f ,0f));
            _cvc.m_Follow = tempPlayer.transform;
            _respawn = false;
        }
    }

    public void Game_Resumed()
    {
        Time.timeScale = 1f;
        _currentState = PlayPauseState.Playing;
    }

    public void Game_Paused()
    {
        if (_currentState != PlayPauseState.Playing)
            return;

        _menu.gameObject.SetActive(true);
        Time.timeScale = 0f;
        _currentState = PlayPauseState.Paused;
    }

    /*
    public PlayPauseState GetCurrentState()
    {
        return GameController.GetInstance()._currentState;
    }
    */
}