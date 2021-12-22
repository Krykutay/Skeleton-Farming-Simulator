using UnityEngine;

public class DarknessTalkOnDeath : MonoBehaviour
{
    [SerializeField] UI_Assistant _darknessTalk;

    string[] _initialDialog;
    SoundManager.SoundTags[] _dialogSounds;
    float[] _typeSpeed;

    void OnEnable()
    {
        Player.Instance.PlayerDied += Player_PlayerDied;
    }

    void OnDisable()
    {
        Player.Instance.PlayerDied += Player_PlayerDied;
    }

    void Player_PlayerDied()
    {
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
