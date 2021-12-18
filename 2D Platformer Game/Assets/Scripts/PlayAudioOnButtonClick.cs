using UnityEngine;
using UnityEngine.UI;

public class PlayAudioOnButtonClick : MonoBehaviour
{
    Button _button;

    void Awake()
    {
        _button = GetComponent<Button>();
    }

    void Start()
    {
        _button.onClick.AddListener(() =>
        {
            SoundManager.Instance.Play(SoundManager.SoundTags.ButtonClick);
        });
    }
}
