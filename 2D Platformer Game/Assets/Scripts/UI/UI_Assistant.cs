using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Assistant : MonoBehaviour
{
    [SerializeField] TMP_Text _messageText;

    Button _button;
    TextWriter.TextWriterSingle _textWriterSingle;
    SoundManager.SoundTags _currentDialogSound;

    void Awake()
    {
        _button = transform.Find("message").GetComponent<Button>();
    }

    public void NpcTalk(string[] messages, SoundManager.SoundTags[] dialogSounds, float[] typeSpeed)
    {
        int count = 1;
        SoundManager.Instance.Play(dialogSounds[0]);
        _currentDialogSound = dialogSounds[0];
        _textWriterSingle = TextWriter.AddWriter_Static(_messageText, messages[0], typeSpeed[0], true, true, StopTalkingSound);

        _button.onClick.RemoveAllListeners();

        _button.onClick.AddListener(() =>
        {
            if (_textWriterSingle != null && _textWriterSingle.IsActive())
            {
                // Currently active TextWriter
                _textWriterSingle.WriteAllAndDestroy();
            }
            else
            {
                if (count >= messages.Length)
                {
                    gameObject.SetActive(false);
                    return;
                }

                SoundManager.Instance.Play(dialogSounds[count]);
                _currentDialogSound = dialogSounds[count];
                _textWriterSingle = TextWriter.AddWriter_Static(_messageText, messages[count], typeSpeed[count], true, true, StopTalkingSound);
                count++;
            }
        });
    }

    public void StopTalkingSound()
    {
        SoundManager.Instance.Stop(_currentDialogSound);
    }

}
