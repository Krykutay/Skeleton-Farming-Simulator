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

    void Awake()
    {
        _button = transform.Find("message").GetComponent<Button>();
    }

    void Start()
    {
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
    }

    void StopTalkingSound()
    {
        SoundManager.Instance.Stop(SoundManager.SoundTags.Talking);
    }

}
