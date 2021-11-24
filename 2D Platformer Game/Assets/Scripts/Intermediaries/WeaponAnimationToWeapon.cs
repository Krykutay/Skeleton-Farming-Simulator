using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimationToWeapon : MonoBehaviour
{
    Weapon _weapon;

    void Awake()
    {
        _weapon = GetComponentInParent<Weapon>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (_weapon is AggressiveWeapon)
            _weapon.AddToDetected(collision);
    }

    void AnimationStartTrigger()
    {
        _weapon.AnimationStartTrigger();
    }

    void AnimationActionTrigger()
    {
        _weapon.AnimationActionTrigger();
    }

    void AnimationFinishTrigger()
    {
        _weapon.AnimationFinishTrigger();
    }

}
