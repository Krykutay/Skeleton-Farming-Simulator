using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NpcTalk2 : MonoBehaviour
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
        _talkText.text = "Listen (" + GameAssets.Instance.keybinds[(int)GameAssets.Keybinds.Interact].text + ")";
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
        if (!_isPlayerInRange || GameManager.Instance.currentState == PlayPauseState.Paused)
            return;

        _initialDialog = new string[]
        {
            "Press [" + GameAssets.Instance.keybinds[(int)GameAssets.Keybinds.Jump].text + "] to jump, jump twice if you like!",              
        };

        _dialogSounds = new SoundManager.SoundTags[]
        {
        SoundManager.SoundTags.NpcTalk2_1,
        };

        _typeSpeed = new float[]
        {
            0.068f,

        };

        _uiAssistant.CloseDarknessTalk();
        _uiAssistant.gameObject.SetActive(true);
        _uiAssistant.NpcTalk(_initialDialog, _dialogSounds, _typeSpeed);

    }

}
