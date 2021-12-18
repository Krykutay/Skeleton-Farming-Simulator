using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSceneManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(Loader.Scene.Loading.ToString());
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
