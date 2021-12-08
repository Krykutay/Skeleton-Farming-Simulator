using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    Animator _anim;
    int _randInt;

    void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        _anim.SetTrigger("move");
        _randInt = Random.Range(0, 3);

        if (_randInt == 0)
            SoundManager.Instance.Play(SoundManager.SoundTags.GrassMove1);
        else if (_randInt == 1)
            SoundManager.Instance.Play(SoundManager.SoundTags.GrassMove2);
        else
            SoundManager.Instance.Play(SoundManager.SoundTags.GrassMove3);
    }
}
