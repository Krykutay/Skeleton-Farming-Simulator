using UnityEngine;
using Cinemachine;

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
    [SerializeField] GameObject[] _panelsToClose;
    [SerializeField] GameObject _gameoverPanel;

    [SerializeField] Transform _spawnPoint;
    [SerializeField] Transform _returnedSpawnPoint;

    CinemachineVirtualCamera _cvc;

    void Awake()
    {
        Instance = this;

        _cvc = transform.parent.Find("Cameras").Find("Player Camera").GetComponent<CinemachineVirtualCamera>();

        currentState = PlayPauseState.Playing;
    }

    void OnEnable()
    {
        Time.timeScale = 1f;

        Player.Instance.OnPlayerDied += Player_PlayerDied;
    }

    void OnDisable()
    {
        Player.Instance.OnPlayerDied += Player_PlayerDied;
        InputManager.Instance.OnPauseAction -= InputManager_PausePressed;
    }

    void Start()
    {
        InputManager.Instance.OnPauseAction += InputManager_PausePressed;

        PlayAmbianceMusicAccordingToScene();
        PlayerSpawnPointAccordingToScene();

        // to prevent a strange webgl bug
        AudioListener.pause = true;
        Time.timeScale = 0f;

        AudioListener.pause = false;
        Time.timeScale = 1f;
    }

    void InputManager_PausePressed()
    {
        if (_gameoverPanel.activeSelf)
            return;

        bool isPanelOn = false;
        foreach (GameObject panel in _panelsToClose)
        {
            if (panel.activeSelf)
            {
                panel.SetActive(false);
                isPanelOn = true;
                SoundManager.Instance.Play(SoundManager.SoundTags.ButtonClick);
            }
        }

        if (!isPanelOn && currentState == PlayPauseState.Playing)
            Game_Paused();
    }

    void Player_PlayerDied()
    {
        Player.Instance.gameObject.SetActive(false);
        Player.Instance.transform.position = _spawnPoint.position;
        _cvc.m_Follow = null;
        _gameoverPanel.SetActive(true);

        SoundManager.Instance.Stop(SoundManager.SoundTags.PlayerRun);
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

    public void LeaveScene2()
    {
        Time.timeScale = 0f;
        _gameoverPanel.SetActive(true);
        currentState = PlayPauseState.Paused;

        StopAmbianceMusicAccordingToScene();
        SoundManager.Instance.Play(SoundManager.SoundTags.Gameover);
    }

    public void Game_Resumed()
    {
        AudioListener.pause = false;
        Time.timeScale = 1f;
        currentState = PlayPauseState.Playing;
    }

    public void Game_Paused()
    {
        if (currentState != PlayPauseState.Playing)
            return;

        AudioListener.pause = true;
        SoundManager.Instance.Play(SoundManager.SoundTags.ButtonClick);
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