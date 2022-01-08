using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NpcTalk5 : MonoBehaviour
{
    [SerializeField] UI_Assistant _uiAssistant;
    [SerializeField] TMP_Text _talkText;

    string[] _initialDialog;
    SoundManager.SoundTags[] _dialogSounds;
    float[] _typeSpeed;

    bool _isPlayerInRange;

    void OnEnable()
    {
        Player.Instance.inputHandler.OnTalkAction += PlayerTalkPressed;
    }

    void OnDisable()
    {
        Player.Instance.inputHandler.OnTalkAction -= PlayerTalkPressed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        _talkText.gameObject.SetActive(true);
        _talkText.text = "Talk (" + GameAssets.Instance.keybinds[(int)GameAssets.Keybinds.Interact].text + ")";
        _isPlayerInRange = true;

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        _talkText.text = "";
        _isPlayerInRange = false;
        _talkText.gameObject.SetActive(false);
        _uiAssistant.gameObject.SetActive(false);
        _uiAssistant.StopTalkingSound();
    }

    void PlayerTalkPressed()
    {
        if (!_isPlayerInRange)
            return;

        _initialDialog = new string[]
        {
            "Not bad so far, now let's learn to Dash!",
            "Press ["+ GameAssets.Instance.keybinds[(int)GameAssets.Keybinds.Dash].text +"] and aim with Mouse, easy peasy.",
            "Then release it or charge it. Remember, dashing can also hurt the eternals, and makes you invincible!",
        };

        _dialogSounds = new SoundManager.SoundTags[]
        {
        SoundManager.SoundTags.NpcTalk5_1,
        SoundManager.SoundTags.NpcTalk5_2,
        SoundManager.SoundTags.NpcTalk5_3,
        };

        _typeSpeed = new float[]
        {
            0.075f,
            0.065f,
            0.06f,
        };

        _uiAssistant.gameObject.SetActive(true);
        _uiAssistant.NpcTalk(_initialDialog, _dialogSounds, _typeSpeed);

    }

}
