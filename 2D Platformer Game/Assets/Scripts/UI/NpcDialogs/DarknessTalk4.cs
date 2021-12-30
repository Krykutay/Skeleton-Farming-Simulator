using System.Collections;
using UnityEngine;

public class DarknessTalk4 : MonoBehaviour
{
    [SerializeField] UI_Assistant _darknessTalk;

    string[] _initialDialog;
    SoundManager.SoundTags[] _dialogSounds;
    float[] _typeSpeed;

    bool isActivatedOnce;

    void OnEnable()
    {
        _darknessTalk.OnSpeechEnd += CoroutineDisableGameObject;
    }

    void OnDisable()
    {
        _darknessTalk.OnSpeechEnd -= CoroutineDisableGameObject;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActivatedOnce)
            return;

        isActivatedOnce = true;

        _initialDialog = new string[]
        {
            "So eager to welcome your death, I see.",
        };

        _dialogSounds = new SoundManager.SoundTags[]
        {
            SoundManager.SoundTags.DarknessTalk4,
        };

        _typeSpeed = new float[]
        {
            0.134f,
        };

        _darknessTalk.gameObject.SetActive(true);
        _darknessTalk.NpcTalk(_initialDialog, _dialogSounds, _typeSpeed);
    }

    void CoroutineDisableGameObject()
    {
        if (isActivatedOnce)
        {
            StopCoroutine(DisableGameObject());
            StartCoroutine(DisableGameObject());
        }

    }

    IEnumerator DisableGameObject()
    {
        yield return new WaitForSeconds(1.8f);

        isActivatedOnce = false;
        _darknessTalk.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

}
