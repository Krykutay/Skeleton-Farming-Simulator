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

    void AnimationTurnOffFlipTrigger()
    {
        _weapon.AnimationTurnOffFlipTrigger();
    }

    void AnimationTurnOnFlipTrigger()
    {
        _weapon.AnimationTurnOnFlipTrigger();
    }


    void AnimationStartMovementTrigger()
    {
        _weapon.AnimationStartMovementTrigger();
    }

    void AnimationStopMovementTrigger()
    {
        _weapon.AnimationStopMovementTrigger();
    }

    void AnimationFinishTrigger()
    {
        _weapon.AnimationFinishTrigger();
    }

    void AnimationActionTrigger()
    {
        _weapon.AnimationActionTrigger();
    }

}
