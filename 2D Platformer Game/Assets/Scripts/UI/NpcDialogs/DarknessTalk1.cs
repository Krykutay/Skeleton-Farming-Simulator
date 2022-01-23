using System.Collections;
using UnityEngine;

public class DarknessTalk1 : DarknessTalk
{
    [SerializeField] UI_Assistant _darknessTalk;

    string[] _initialDialog;
    SoundManager.SoundTags[] _dialogSounds;
    float[] _typeSpeed;

    static bool _IsActivatedOnce;

    void OnEnable()
    {
        if (_IsActivatedOnce)
        {
            _darknessTalk.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }

        _darknessTalk.OnSpeechEnd += CoroutineDisableGameObject;
    }

    void OnDisable()
    {
        _darknessTalk.OnSpeechEnd -= CoroutineDisableGameObject;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (_IsActivatedOnce)
            return;

        _IsActivatedOnce = true;

        _initialDialog = new string[]
        {
            "I will eradicate you, pathetic living.",
        };

        _dialogSounds = new SoundManager.SoundTags[]
        {
            SoundManager.SoundTags.DarknessTalk1,
        };

        _typeSpeed = new float[]
        {
            0.12f,
        };

        _darknessTalk.gameObject.SetActive(true);
        _darknessTalk.NpcTalk(_initialDialog, _dialogSounds, _typeSpeed);
    }

    void CoroutineDisableGameObject()
    {
        if (_IsActivatedOnce)
        {
            if (disableGameobject != null)
                StopCoroutine(disableGameobject);
            disableGameobject = StartCoroutine(DisableGameObject());
        }
    }

    IEnumerator DisableGameObject()
    {
        yield return new WaitForSeconds(1.2f);

        _darknessTalk.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

}
