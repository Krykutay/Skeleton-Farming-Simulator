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
    [SerializeField] GameObject _gameoverPanel;
    [SerializeField] CanvasScaler _canvasScaler;

    [SerializeField] Transform _respawnPoint;

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
        Player.Instance.OnPlayerDied += Player_PlayerDied;
    }

    void OnDisable()
    {
        Player.Instance.OnPlayerDied += Player_PlayerDied;
    }

    void Start()
    {
        SoundManager.Instance.Play(SoundManager.SoundTags.Ambiance2);
    }

    void Update()
    {
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
        Player.Instance.gameObject.SetActive(false);
        Player.Instance.transform.position = _respawnPoint.position;
        _cvc.m_Follow = null;
        _gameoverPanel.SetActive(true);

        SoundManager.Instance.Stop(SoundManager.SoundTags.Ambiance2);
        SoundManager.Instance.Play(SoundManager.SoundTags.Ambiance4);
    }

    public void Respawn()
    {
        _cvc.m_Follow = Player.Instance.transform;
        Player.Instance.gameObject.SetActive(true);

        SoundManager.Instance.Stop(SoundManager.SoundTags.Ambiance4);
        SoundManager.Instance.Play(SoundManager.SoundTags.Ambiance2);
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

}