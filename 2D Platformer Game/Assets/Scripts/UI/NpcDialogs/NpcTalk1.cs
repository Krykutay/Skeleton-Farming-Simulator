using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NpcTalk1 : MonoBehaviour
{
    [SerializeField] UI_Assistant _uiAssistant;
    [SerializeField] TMP_Text _talkText;

    string[] _initialDialog;
    SoundManager.SoundTags[] _dialogSounds;
    float[] _typeSpeed;

    bool _isPlayerInRange;

    void OnEnable()
    {
        Player.Instance.inputHandler.talkAction += PlayerTalkPressed;
    }

    void OnDisable()
    {
        Player.Instance.inputHandler.talkAction -= PlayerTalkPressed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        _talkText.gameObject.SetActive(true);
        _talkText.text = "Talk (E)";
        _isPlayerInRange = true;

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        _talkText.text = "";
        _isPlayerInRange = false;
        _talkText.gameObject.SetActive(true);
        _uiAssistant.gameObject.SetActive(false);
        _uiAssistant.StopTalkingSound();
    }

    void PlayerTalkPressed()
    {
        if (!_isPlayerInRange)
            return;

        _initialDialog = new string[]
        {
            "Welcome stranger, I.. mean hero!. We've been waiting for you!",
            "Our mining pits are under attack. The eternals have almost taken the complete control of them.",
            "We've managed to capture some of those, and we would like to train you against them.",
            "Let's move on, and begin your training.",
        };

        _dialogSounds = new SoundManager.SoundTags[]
        {
        SoundManager.SoundTags.NpcTalk1_1,
        SoundManager.SoundTags.NpcTalk1_2,
        SoundManager.SoundTags.NpcTalk1_3,
        SoundManager.SoundTags.NpcTalk1_4,
        };

        _typeSpeed = new float[]
        {
            0.07f,
            0.062f,
            0.05f,
            0.062f,
        };

        _uiAssistant.gameObject.SetActive(true);
        _uiAssistant.NpcTalk(_initialDialog, _dialogSounds, _typeSpeed );
        
    }

}
