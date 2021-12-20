using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
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
