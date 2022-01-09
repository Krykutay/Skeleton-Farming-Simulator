using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NpcTalk6 : MonoBehaviour
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
            "Ah, the eternals.. Let's get you to fighting now.",
            "Press ["+ GameAssets.Instance.keybinds[(int)GameAssets.Keybinds.Attack].text +"] to attack, " +
            "[" + GameAssets.Instance.keybinds[(int)GameAssets.Keybinds.Parry].text + "] to parry the incoming attack.",
            "If you time it well, you perform a Perfect Parry and take no hit, and stun the enemy.",
            "All you need to do is parry right before the incoming attack, and that's all!",
        };

        _dialogSounds = new SoundManager.SoundTags[]
        {
        SoundManager.SoundTags.NpcTalk6_1,
        SoundManager.SoundTags.NpcTalk6_2,
        SoundManager.SoundTags.NpcTalk6_3,
        SoundManager.SoundTags.NpcTalk6_4,
        };

        _typeSpeed = new float[]
        {
            0.062f,
            0.06f,
            0.06f,
            0.055f,
        };

        _uiAssistant.CloseDarknessTalk();
        _uiAssistant.gameObject.SetActive(true);
        _uiAssistant.NpcTalk(_initialDialog, _dialogSounds, _typeSpeed);

    }

}
