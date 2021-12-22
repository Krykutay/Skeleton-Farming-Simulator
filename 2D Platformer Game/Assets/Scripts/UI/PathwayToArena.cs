using UnityEngine;
using TMPro;

public class PathwayToArena : MonoBehaviour
{
    [SerializeField] TMP_Text _talkText;

    bool _isPlayerInRange;

    void OnEnable()
    {
        Player.Instance.inputHandler.talkAction += PlayerTalkPressed;
    }

    void OnDisable()
    {
        Player.Instance.inputHandler.talkAction -= PlayerTalkPressed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        _talkText.text = "Talk (E)";
        _isPlayerInRange = true;

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        _talkText.text = "";
        _isPlayerInRange = false;
    }

    void PlayerTalkPressed()
    {
        if (!_isPlayerInRange)
            return;

        // Proceed to Arena

    }

}
