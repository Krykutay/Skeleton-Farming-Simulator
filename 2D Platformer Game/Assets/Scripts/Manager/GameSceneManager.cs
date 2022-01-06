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

    public void LoadScene1()
    {
        Time.timeScale = 1f;
        ApplicationModel.PreviousScene = ApplicationModel.CurrentScene;
        ApplicationModel.CurrentScene = (int)Loader.Scene.Scene1;
        SceneManager.LoadScene(Loader.Scene.Loading.ToString());
    }

    public void LoadScene2()
    {
        Time.timeScale = 1f;
        ApplicationModel.PreviousScene = ApplicationModel.CurrentScene;
        ApplicationModel.CurrentScene = (int)Loader.Scene.Scene2;
        SceneManager.LoadScene(Loader.Scene.Loading.ToString());
    }
}
