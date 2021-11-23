using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressiveWeapon : Weapon
{
    protected SO_AggressiveWeaponData aggressiveWeaponData;

    List<IDamageable> _detectedDamageables = new List<IDamageable>();

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();

        if (weaponData is SO_AggressiveWeaponData)
            aggressiveWeaponData = (SO_AggressiveWeaponData)weaponData;
        else
            Debug.LogError("Wrong data for the weapon");
    }

    public override void AnimationActionTrigger()
    {
        base.AnimationActionTrigger();

        CheckMeleeAttack();
    }

    void CheckMeleeAttack()
    {
        WeaponAttackDetails detials = aggressiveWeaponData.attackDetails[attackCounter];

        foreach (IDamageable target in _detectedDamageables)
        {
            target.Damage(detials.damageAmount);
        }
    }

    public void AddToDetected(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamageable>(out var damageable))
        {
            _detectedDamageables.Add(damageable);
        }
    }

    public void RemoveFromDetected(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamageable>(out var damageable))
        {
            _detectedDamageables.Remove(damageable);
        }
    }

}
