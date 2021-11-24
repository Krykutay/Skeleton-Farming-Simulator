using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTestDummy : MonoBehaviour, IDamageable
{
    Animator _anim;

    void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    public bool Damage(AttackDetails attackDetails)
    {
        HitParticlePool.Instance.Get(transform.position, Quaternion.Euler(0f, 0f, Random.Range(0, 360)));
        _anim.SetTrigger("damage");

        return true;
    }

}
