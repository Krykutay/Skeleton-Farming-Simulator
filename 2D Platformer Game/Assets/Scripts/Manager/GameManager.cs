using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public enum PlayPauseState
{
    Playing,
    Paused,
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public PlayPauseState currentState { get; private set; }

    [SerializeField] GameObject _menu;
    [SerializeField] GameObject _shop;
    //[SerializeField] GameObject _gameoverPanel;
    [SerializeField] GameObject _player;
    [SerializeField] CanvasScaler _canvasScaler;

    [SerializeField] Transform _respawnPoint;

    [SerializeField] float _respawnTime;

    float _respawnTimeStart;

    bool _respawn;

    CinemachineVirtualCamera _cvc;

    void Awake()
    {
        Instance = this;

        _cvc = transform.parent.Find("Cameras").Find("Player Camera").GetComponent<CinemachineVirtualCamera>();

        currentState = PlayPauseState.Playing;
        _canvasScaler.referenceResolution = new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);
    }

    void OnEnable()
    {
        Player.Instance.PlayerDied += Player_PlayerDied;
    }

    void OnDisable()
    {
        Player.Instance.PlayerDied += Player_PlayerDied;
    }

    void Start()
    {
        SoundManager.Instance.Play(SoundManager.SoundTags.Ambiance2);
    }

    void Update()
    {
        CheckRespawn();

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (_shop.activeSelf)
                _shop.gameObject.SetActive(false);
            else
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
        currentState = PlayPauseState.Playing;
    }

    public void Game_Paused()
    {
        if (currentState != PlayPauseState.Playing)
            return;

        _menu.gameObject.SetActive(true);
        Time.timeScale = 0f;
        currentState = PlayPauseState.Paused;
    }

    /*
    public PlayPauseState GetCurrentState()
    {
        return GameController.GetInstance()._currentState;
    }
    */
}