using UnityEngine;
using TMPro;

public class PathwayToScene1 : MonoBehaviour
{
    [SerializeField] TMP_Text _talkText;
    [SerializeField] GameObject _toScene1Popup;

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
        _talkText.text = "To Traning Ground (E)";
        _isPlayerInRange = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        _talkText.text = "";
        _isPlayerInRange = false;

        if (_talkText != null)
            _talkText.gameObject.SetActive(false);
        if (_toScene1Popup != null)
            _toScene1Popup.SetActive(false);
    }

    void PlayerTalkPressed()
    {
        if (!_isPlayerInRange)
            return;

        _toScene1Popup.SetActive(true);
    }

}
