using System.Collections;
using UnityEngine;
using TMPro;

public class Scene1Gems : MonoBehaviour
{
    [SerializeField] protected int minModifier = 40;
    [SerializeField] protected int maxModifier = 60;

    [SerializeField] TMP_Text _talkText;

    Transform orb1;
    Transform orb2;
    Transform orb3;

    Vector3 _velocity = Vector3.zero;

    bool isFollowing;
    bool _isPlayerInRange;

    void Awake()
    {
        if (PlayerPrefs.HasKey("scene1Gems"))
            Destroy(gameObject);

        orb1 = transform.Find("Orb1");
        orb2 = transform.Find("Orb2");
        orb3 = transform.Find("Orb3");
    }

    void OnEnable()
    {
        Player.Instance.inputHandler.OnTalkAction += PlayerInteractPressed;
    }

    void OnDisable()
    {
        Player.Instance.inputHandler.OnTalkAction -= PlayerInteractPressed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (PlayerPrefs.HasKey("scene1Gems"))
            return;

        _talkText.gameObject.SetActive(true);
        _talkText.text = "Collect (E)";
        _isPlayerInRange = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        _talkText.text = "";
        _isPlayerInRange = false;
        _talkText.gameObject.SetActive(false);
    }

    void PlayerInteractPressed()
    {
        if (!_isPlayerInRange)
            return;

        StartFollowing();
        StartCoroutine(CollectGems());
        StartCoroutine(DelayBeforeDisappear());
    }

    IEnumerator CollectGems()
    {
        while (isFollowing)
        {
            orb1.transform.position = Vector3.SmoothDamp(transform.position, Player.Instance.transform.position, ref _velocity, Time.deltaTime * Random.Range(minModifier, maxModifier));
            orb2.transform.position = Vector3.SmoothDamp(transform.position, Player.Instance.transform.position, ref _velocity, Time.deltaTime * Random.Range(minModifier, maxModifier));
            orb3.transform.position = Vector3.SmoothDamp(transform.position, Player.Instance.transform.position, ref _velocity, Time.deltaTime * Random.Range(minModifier, maxModifier));
            yield return null;
        }
    }

    IEnumerator DelayBeforeDisappear()
    {
        yield return new WaitForSeconds(0.25f);

        isFollowing = false;
        StopCoroutine(CollectGems());
        ScoreManager.Instance.Token_Earned(20);
        PlayerPrefs.SetInt("scene1Gems", 1);
        Destroy(gameObject);
    }

    public void StartFollowing()
    {
        isFollowing = true;
        int playSoundEffect = Random.Range(0, 3);
        if (playSoundEffect == 0)
            SoundManager.Instance.Play(SoundManager.SoundTags.DropLoot1);
        else if (playSoundEffect == 1)
            SoundManager.Instance.Play(SoundManager.SoundTags.DropLoot2);
        else
            SoundManager.Instance.Play(SoundManager.SoundTags.DropLoot3);
    }
}
