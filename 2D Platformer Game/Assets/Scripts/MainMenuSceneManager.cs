using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSceneManager : MonoBehaviour
{
    void Start()
    {
        SoundManager.Instance.Play(SoundManager.SoundTags.Ambiance1);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(Loader.Scene.Loading.ToString());
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
