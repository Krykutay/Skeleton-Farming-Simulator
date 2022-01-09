using UnityEngine;
using TMPro;
using System.Collections;

public class EasterEgg : MonoBehaviour
{
    [SerializeField] TMP_Text _talkText;
    [SerializeField] Animator _easterEggAnim;
    [SerializeField] GameObject _easterEggPowerups;

    bool _isPlayerInRange;
    bool _isPressedOnce;

    void OnEnable()
    {
        _isPressedOnce = false;
        Player.Instance.inputHandler.OnTalkAction += PlayerTalkPressed;
    }

    void OnDisable()
    {
        Player.Instance.inputHandler.OnTalkAction -= PlayerTalkPressed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isPressedOnce)
            return;

        _talkText.gameObject.SetActive(true);
        _talkText.text = "Press (" + GameAssets.Instance.keybinds[(int)GameAssets.Keybinds.Interact].text + ")";
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
        if (!_isPlayerInRange || GameManager.Instance.currentState == PlayPauseState.Paused)
            return;

        if (_isPressedOnce)
            return;

        _isPressedOnce = true;
        _talkText.text = "";
        _talkText.gameObject.SetActive(false);
        SoundManager.Instance.Play(SoundManager.SoundTags.SkeletonRespawn);
        _easterEggAnim.SetTrigger("easterEgg");

        StartCoroutine(SpawnPowerups());
    }

    IEnumerator SpawnPowerups()
    {
        yield return new WaitForSeconds(2.5f);

        _easterEggPowerups.SetActive(true);
    }

}
