using System.Collections;
using UnityEngine;
using TMPro;

public class Scene1Gems : MonoBehaviour
{
    [SerializeField] int _minModifier = 40;
    [SerializeField] int _maxModifier = 60;

    [SerializeField] int _tokensEarned = 18;

    [SerializeField] TMP_Text _talkText;

    Animator _anim;

    Transform orb1;
    Transform orb2;
    Transform orb3;

    Vector3 _velocity = Vector3.zero;
    Vector3 _velocity1 = Vector3.zero;
    Vector3 _velocity2 = Vector3.zero;

    bool isFollowing;
    bool _isPlayerInRange;

    void Awake()
    {
        if (PlayerPrefs.HasKey("scene1Gems"))
            Destroy(gameObject);

        _anim = GetComponent<Animator>();

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
        _talkText.text = "Collect (" + GameAssets.Instance.keybinds[(int)GameAssets.Keybinds.Interact].text + ")";
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

        _anim.enabled = false;
        StartFollowing();
        StartCoroutine(CollectGems());
        StartCoroutine(DelayBeforeDisappear());
    }

    IEnumerator CollectGems()
    {
        while (isFollowing)
        {
            orb1.transform.position = Vector3.SmoothDamp(orb1.transform.position, Player.Instance.transform.position, ref _velocity, Time.deltaTime * Random.Range(_minModifier, _maxModifier));
            orb2.transform.position = Vector3.SmoothDamp(orb2.transform.position, Player.Instance.transform.position, ref _velocity1, Time.deltaTime * Random.Range(_minModifier, _maxModifier));
            orb3.transform.position = Vector3.SmoothDamp(orb3.transform.position, Player.Instance.transform.position, ref _velocity2, Time.deltaTime * Random.Range(_minModifier, _maxModifier));
            yield return null;
        }
    }

    IEnumerator DelayBeforeDisappear()
    {
        yield return new WaitForSeconds(0.3f);

        isFollowing = false;
        StopCoroutine(CollectGems());
        ScoreManager.Instance.Token_Earned(_tokensEarned);
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
