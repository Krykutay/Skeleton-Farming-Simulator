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

    [SerializeField] Transform _spawnPoint;
    [SerializeField] Transform _returnedSpawnPoint;

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
        PlayAmbianceMusicAccordingToScene();
        PlayerSpawnPointAccordingToScene();
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
        Player.Instance.transform.position = _spawnPoint.position;
        _cvc.m_Follow = null;
        _gameoverPanel.SetActive(true);

        StopAmbianceMusicAccordingToScene();
        SoundManager.Instance.Play(SoundManager.SoundTags.Gameover);
    }

    public void Respawn()
    {
        _cvc.m_Follow = Player.Instance.transform;
        Player.Instance.gameObject.SetActive(true);

        SoundManager.Instance.Stop(SoundManager.SoundTags.Gameover);
        PlayAmbianceMusicAccordingToScene();
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

    void PlayAmbianceMusicAccordingToScene()
    {
        if (ApplicationModel.CurrentScene == (int)Loader.Scene.Scene1)
            SoundManager.Instance.Play(SoundManager.SoundTags.Ambiance2);
        else
            SoundManager.Instance.Play(SoundManager.SoundTags.Ambiance3);
    }

    void StopAmbianceMusicAccordingToScene()
    {
        if (ApplicationModel.CurrentScene == (int)Loader.Scene.Scene1)
            SoundManager.Instance.Stop(SoundManager.SoundTags.Ambiance2);
        else
            SoundManager.Instance.Stop(SoundManager.SoundTags.Ambiance3);
    }

    void PlayerSpawnPointAccordingToScene()
    {
        if (ApplicationModel.PreviousScene == (int)Loader.Scene.Scene1)
        {
            Player.Instance.transform.position = _spawnPoint.position;
        }
        else if (ApplicationModel.PreviousScene == (int)Loader.Scene.Scene2)
        {
            Player.Instance.transform.position = _returnedSpawnPoint.position;
        }
    }

}