using System.Collections;
using UnityEngine;

public class DarknessTalkOnDeath : MonoBehaviour
{
    [SerializeField] UI_Assistant _darknessTalk;

    string[] _initialDialog;
    SoundManager.SoundTags[] _dialogSounds;
    float[] _typeSpeed;

    bool isPlayerDead;

    void OnEnable()
    {
        Player.Instance.OnPlayerDied += Player_PlayerDied;
        _darknessTalk.OnSpeechEnd += CoroutineDisableGameObject;
    }

    void OnDisable()
    {
        Player.Instance.OnPlayerDied -= Player_PlayerDied;
        _darknessTalk.OnSpeechEnd -= CoroutineDisableGameObject;
    }

    void Player_PlayerDied()
    {
        isPlayerDead = true;

        _initialDialog = new string[]
        {
            "Your last breath is my blessing.",
        };

        _dialogSounds = new SoundManager.SoundTags[]
        {
            SoundManager.SoundTags.DarknessTalk4,
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
        if (isPlayerDead)
            StartCoroutine(DisableGameObject());
    }

    IEnumerator DisableGameObject()
    {
        yield return new WaitForSeconds(1.8f);

        isPlayerDead = false;
        _darknessTalk.gameObject.SetActive(false);
    }

}
