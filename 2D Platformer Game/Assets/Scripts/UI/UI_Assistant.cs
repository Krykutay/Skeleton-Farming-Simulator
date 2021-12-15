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

    void Start()
    {
        /*
        SoundManager.Instance.Play(SoundManager.SoundTags.Talking);
        _textWriterSingle = TextWriter.AddWriter_Static(_messageText, "Aloha", 0.05f, true, true, StopTalkingSound);

        _button.onClick.AddListener(() =>
        {
            if (_textWriterSingle != null && _textWriterSingle.IsActive())
            {
                // Currently active TextWriter
                _textWriterSingle.WriteAllAndDestroy();
            }
            else
            {
                string[] messageArray = new string[]
                {
                "This is the assistant speaking, hello and goodbye, see you next time!",
                "Hey there!",
                "This is a really cool and usefull effect",
                "Let's learn some code and make awesome games!",
                };

                string message = messageArray[Random.Range(0, messageArray.Length)];
                SoundManager.Instance.Play(SoundManager.SoundTags.Talking);
                _textWriterSingle = TextWriter.AddWriter_Static(_messageText, message, 0.05f, true, true, StopTalkingSound);
            }

        });
        */
    }

    public void NpcTalk(string[] messages, SoundManager.SoundTags[] dialogSounds)
    {
        int count = 1;
        SoundManager.Instance.Play(dialogSounds[0]);
        _currentDialogSound = dialogSounds[0];
        _textWriterSingle = TextWriter.AddWriter_Static(_messageText, messages[0], 0.05f, true, true, StopTalkingSound);

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
                _textWriterSingle = TextWriter.AddWriter_Static(_messageText, messages[count], 0.05f, true, true, StopTalkingSound);
                count++;
            }
        });
    }

    void StopTalkingSound()
    {
        SoundManager.Instance.Stop(_currentDialogSound);
    }

}
