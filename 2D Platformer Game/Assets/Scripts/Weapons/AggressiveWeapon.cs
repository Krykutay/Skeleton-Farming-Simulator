using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressiveWeapon : Weapon
{
    protected SO_AggressiveWeaponData aggressiveWeaponData;

    List<IDamageable> _detectedDamageables = new List<IDamageable>();

    Collider2D _weaponCollider;

    protected override void Awake()
    {
        base.Awake();

        _weaponCollider = transform.Find("Weapon").GetComponent<Collider2D>();
    }

    protected override void Start()
    {
        base.Start();

        if (weaponData is SO_AggressiveWeaponData)
            aggressiveWeaponData = (SO_AggressiveWeaponData)weaponData;
        else
            Debug.LogError("Wrong data for the weapon");
    }

    void CheckMeleeAttack()
    {
        AttackDetails attackDetails = aggressiveWeaponData.attackDetails[attackCounter];

        attackDetails.position = transform.position;

        foreach (IDamageable target in _detectedDamageables)
        {
            target.Damage(attackDetails);
        }
    }

    public override void AddToDetected(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamageable>(out var damageable))
        {
            _detectedDamageables.Add(damageable);
        }
    }

    public override void AnimationStartTrigger()
    {
        base.AnimationStartTrigger();

        _weaponCollider.enabled = true;
    }

    public override void AnimationActionTrigger()
    {
        base.AnimationActionTrigger();

        CheckMeleeAttack();
        _weaponCollider.enabled = false;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        _detectedDamageables.Clear();
    }

    public override void AnimationCancelled()
    {
        base.AnimationCancelled();
        _detectedDamageables.Clear();
    }
}
