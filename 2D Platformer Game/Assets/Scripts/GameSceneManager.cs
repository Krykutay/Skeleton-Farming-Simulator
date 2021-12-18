using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    static GameSceneManager _instance;

    public static GameSceneManager GetInstance()
    {
        return _instance;
    }

    void Awake()
    {
        _instance = this;
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(Loader.Scene.MainMenu.ToString());
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(Loader.Scene.Loading.ToString());
    }
}
