using UnityEngine;

public class PlayIdleAnim : MonoBehaviour
{
    Animator _anim;

    void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    void Start()
    {
        _anim.SetBool("idle", false);
        _anim.SetBool("idle", true);
    }
}
