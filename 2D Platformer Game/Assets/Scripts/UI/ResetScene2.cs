using UnityEngine;
using TMPro;

public class ResetScene2 : MonoBehaviour
{
    [SerializeField] TMP_Text _talkText;
    [SerializeField] GameObject _leavePopup;

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
        _talkText.text = "Leave (" + GameAssets.Instance.keybinds[(int)GameAssets.Keybinds.Interact].text + ")";
        _isPlayerInRange = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        _talkText.text = "";
        _isPlayerInRange = false;

        if (_talkText != null)
            _talkText.gameObject.SetActive(false);
        if (_leavePopup != null)
            _leavePopup.SetActive(false);
    }

    void PlayerTalkPressed()
    {
        if (!_isPlayerInRange)
            return;

        _leavePopup.SetActive(true);
    }

}
