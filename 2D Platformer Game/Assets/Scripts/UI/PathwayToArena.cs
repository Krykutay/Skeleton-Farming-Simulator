using UnityEngine;
using TMPro;

public class PathwayToArena : MonoBehaviour
{
    [SerializeField] TMP_Text _talkText;

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
        _talkText.text = "Talk (E)";
        _isPlayerInRange = true;

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        _talkText.text = "";
        _talkText.gameObject.SetActive(false);
        _isPlayerInRange = false;
    }

    void PlayerTalkPressed()
    {
        if (!_isPlayerInRange)
            return;

        // Proceed to Arena

    }

}
