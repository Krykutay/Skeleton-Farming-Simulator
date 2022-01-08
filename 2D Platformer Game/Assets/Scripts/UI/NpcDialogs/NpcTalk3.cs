using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NpcTalk3 : MonoBehaviour
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
        if (!_isPlayerInRange)
            return;

        _initialDialog = new string[]
        {
            "Touch the wall and Press ["+ GameAssets.Instance.keybinds[(int)GameAssets.Keybinds.Jump].text +"], and that's, Wall Jump eh!",
            "Push the wall with ["+ GameAssets.Instance.keybinds[(int)GameAssets.Keybinds.MoveLeft].text +"] " +
            "or ["+ GameAssets.Instance.keybinds[(int)GameAssets.Keybinds.MoveRight].text +"] to Slide on the wall, it's super fun!"
        };

        _dialogSounds = new SoundManager.SoundTags[]
        {
        SoundManager.SoundTags.NpcTalk3_1,
        SoundManager.SoundTags.NpcTalk3_2,
        };

        _typeSpeed = new float[]
        {
            0.06f,
            0.058f,
        };


        _uiAssistant.gameObject.SetActive(true);
        _uiAssistant.NpcTalk(_initialDialog, _dialogSounds, _typeSpeed);

    }

}
