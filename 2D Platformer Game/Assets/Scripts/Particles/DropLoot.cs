using UnityEngine;

public class DropLoot : MonoBehaviour
{
    [SerializeField] protected int minModifier = 40;
    [SerializeField] protected int maxModifier = 60;

    Vector3 _velocity = Vector3.zero;

    protected bool isFollowing;

    Animator _anim;

    void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    void OnEnable()
    {
        isFollowing = false;
    }

    void Update()
    {
        if (isFollowing && Player.Instance.currentHealth > 0f)
        {
            transform.position = Vector3.SmoothDamp(transform.position, Player.Instance.transform.position, ref _velocity, Time.deltaTime * Random.Range(minModifier, maxModifier));
        }
    }

    public void StartFollowing()
    {
        if (Player.Instance.currentHealth <= 0f)
        {
            _anim.Play("DropLoot", -1, 0f);
            return;
        }

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
