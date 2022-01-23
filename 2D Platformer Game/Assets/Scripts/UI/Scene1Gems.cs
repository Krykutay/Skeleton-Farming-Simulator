using System.Collections;
using UnityEngine;
using TMPro;

public class Scene1Gems : MonoBehaviour
{
    enum Gems
    {
        StartGems,
        EndGems
    }

    [SerializeField] Gems _gems;

    [SerializeField] float _speed = 0.25f;
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

    Coroutine _collectGems = null;

    void Awake()
    {
        if (PlayerPrefs.HasKey("scene1StartGems") && _gems == Gems.StartGems)
            Destroy(gameObject);

        if (PlayerPrefs.HasKey("scene1EndGems") && _gems == Gems.EndGems)
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
        if (PlayerPrefs.HasKey("scene1StartGems") && _gems == Gems.StartGems)
            return;

        if (PlayerPrefs.HasKey("scene1EndGems") && _gems == Gems.EndGems)
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
        _collectGems = StartCoroutine(CollectGems());
        StartCoroutine(DelayBeforeDisappear());
    }

    IEnumerator CollectGems()
    {
        while (isFollowing)
        {
            orb1.transform.position = Vector3.SmoothDamp(orb1.transform.position, Player.Instance.transform.position, ref _velocity, _speed);
            orb2.transform.position = Vector3.SmoothDamp(orb2.transform.position, Player.Instance.transform.position, ref _velocity1, _speed);
            orb3.transform.position = Vector3.SmoothDamp(orb3.transform.position, Player.Instance.transform.position, ref _velocity2, _speed);
            yield return null;
        }
    }

    IEnumerator DelayBeforeDisappear()
    {
        yield return new WaitForSeconds(0.3f);

        isFollowing = false;
        if (_collectGems != null)
            StopCoroutine(_collectGems);
        ScoreManager.Instance.Token_Earned(_tokensEarned);

        if (_gems == Gems.StartGems)
            PlayerPrefs.SetInt("scene1StartGems", 1);

        if (_gems == Gems.EndGems)
            PlayerPrefs.SetInt("scene1EndGems", 1);

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
