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

    void AnimationFinishTrigger()
    {
        _weapon.AnimationFinishTrigger();
    }

}
