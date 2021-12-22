using UnityEngine;

public class DarknessTalk3 : MonoBehaviour
{
    [SerializeField] UI_Assistant _darknessTalk;

    string[] _initialDialog;
    SoundManager.SoundTags[] _dialogSounds;
    float[] _typeSpeed;

    void OnTriggerEnter2D(Collider2D collision)
    {
        _initialDialog = new string[]
        {
            "My fury is upon you, mortal!",
        };

        _dialogSounds = new SoundManager.SoundTags[]
        {
            SoundManager.SoundTags.DarknessTalk3,
        };

        _typeSpeed = new float[]
        {
            0.068f,
        };

        _darknessTalk.gameObject.SetActive(true);
        _darknessTalk.NpcTalk(_initialDialog, _dialogSounds, _typeSpeed);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        _darknessTalk.gameObject.SetActive(false);
    }

}
