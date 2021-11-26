using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressiveWeapon : Weapon
{
    [SerializeField] LayerMask _damageable;

    protected SO_AggressiveWeaponData aggressiveWeaponData;

    Transform _attackPosition;

    protected override void Awake()
    {
        base.Awake();

        _attackPosition = transform.Find("AttackPosition");
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

        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(_attackPosition.position, attackDetails.attackRadius, _damageable);

        foreach (Collider2D collider in detectedObjects)
        {
            if (collider.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.Damage(attackDetails);
            }
        }
    }

    public override void AnimationActionTrigger()
    {
        base.AnimationActionTrigger();

        CheckMeleeAttack();
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationCancelled()
    {
        base.AnimationCancelled();
    }

    void OnDrawGizmos()
    {
        if(_attackPosition != null)
        {
            Gizmos.DrawWireSphere(_attackPosition.position, ((SO_AggressiveWeaponData)weaponData).attackDetails[0].attackRadius);
            Gizmos.DrawWireSphere(_attackPosition.position, ((SO_AggressiveWeaponData)weaponData).attackDetails[1].attackRadius);
        }
    }
}
