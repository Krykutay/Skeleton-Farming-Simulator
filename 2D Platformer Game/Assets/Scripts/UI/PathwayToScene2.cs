using UnityEngine;
using TMPro;

public class PathwayToScene2 : MonoBehaviour
{
    [SerializeField] TMP_Text _talkText;
    [SerializeField] GameObject _toScene2Popup;

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
        _talkText.text = "To Mines (E)";
        _isPlayerInRange = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        _talkText.text = "";
        _isPlayerInRange = false;

        if (_talkText != null)
            _talkText.gameObject.SetActive(false);
        if (_toScene2Popup != null)
            _toScene2Popup.SetActive(false);
    }

    void PlayerTalkPressed()
    {
        if (!_isPlayerInRange)
            return;

        _toScene2Popup.SetActive(true);
    }

}
