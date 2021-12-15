using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    [SerializeField] UI_Assistant uiAssistant;

    string[] _initialDialog;
    SoundManager.SoundTags[] _dialogSounds;

    IEnumerator Start()
    {
        _initialDialog = new string[]
        {
                "This is the assistant speaking, hello and goodbye, see you next time!",
                "Hey there!",
                "This is a really cool and usefull effect",
                "Let's learn some code and make awesome games!",
        };

        _dialogSounds = new SoundManager.SoundTags[]
        {
            SoundManager.SoundTags.Talking,
            SoundManager.SoundTags.Talking,
            SoundManager.SoundTags.Talking,
            SoundManager.SoundTags.Talking,
        };

        while (Vector3.Distance(Player.Instance.transform.position, transform.position) > 5f)
        {
            yield return null;
        }

        uiAssistant.gameObject.SetActive(true);
        uiAssistant.NpcTalk(_initialDialog, _dialogSounds);
    }

}
