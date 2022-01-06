using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Loader : MonoBehaviour
{
    [SerializeField] Slider _loadingSlider;
    [SerializeField] TMP_Text progressText;

    public enum Scene
    {
        MainMenu,
        Loading,
        Scene1,
        Scene2,
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);  // Merely waiting for people to get rick rolled - for the memes

        AsyncOperation operation;

        if (ApplicationModel.CurrentScene == (int)Scene.Scene1)
            operation = SceneManager.LoadSceneAsync(Scene.Scene1.ToString());
        else
            operation = SceneManager.LoadSceneAsync(Scene.Scene2.ToString());


        while (operation.isDone == false)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            _loadingSlider.value = progress;
            progressText.text = progress * 100f + "%";

            yield return null;
        }
    }

}
