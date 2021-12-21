using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DarknessTalk1 : MonoBehaviour
{
    [SerializeField] UI_Assistant _uiAssistant;

    string[] _initialDialog;
    SoundManager.SoundTags[] _dialogSounds;
    float[] _typeSpeed;

    void OnTriggerEnter2D(Collider2D collision)
    {
        _initialDialog = new string[]
        {
            "Press to jump, jump twice if you like!",
        };

        _dialogSounds = new SoundManager.SoundTags[]
        {
            SoundManager.SoundTags.NpcTalk2_1,
        };

        _typeSpeed = new float[]
        {
            0.068f,
        };

        _uiAssistant.gameObject.SetActive(true);
        _uiAssistant.NpcTalk(_initialDialog, _dialogSounds, _typeSpeed);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        _uiAssistant.gameObject.SetActive(false);
    }

}
