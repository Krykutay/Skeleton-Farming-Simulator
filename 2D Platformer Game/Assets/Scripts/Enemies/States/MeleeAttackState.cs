using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackState : AttackState
{
    protected D_MeleeAttackState stateData;

    protected AttackDetails[] attackDetails;
    protected float meleeAttackCooldown;

    protected int meleeAttackType = 0;

    float meleeAttackFinishTime = Mathf.NegativeInfinity;
    bool _attackStance;

    public MeleeAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_MeleeAttackState stateData) : base(entity, stateMachine, animBoolName, attackPosition)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        entity.CheckIfShouldFlip();

        attackDetails = stateData.attackDetails;
        meleeAttackCooldown = stateData.meleeAttackCooldown;

        _attackStance = false;
        entity.anim.SetBool("meleeAttack", false);
        entity.anim.SetBool("idle", true);
    }

    public override void Exit()
    {
        base.Exit();

        entity.anim.SetBool("meleeAttack", false);
        entity.anim.SetBool("idle", false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= meleeAttackFinishTime + meleeAttackCooldown && !_attackStance)
        {
            _attackStance = true;
            entity.anim.SetBool("idle", false);
            entity.anim.SetInteger("meleeAttackType", meleeAttackType);
            entity.anim.SetBool("meleeAttack", true);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position, attackDetails[meleeAttackType].attackRadius, entity.entityData.player);

        foreach (Collider2D collider in detectedObjects)
        {
            if (collider.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.Damage(attackDetails[meleeAttackType]);
            }
        }
    }

    public override void FinishAttack()
    {
        base.FinishAttack();

        meleeAttackFinishTime = Time.time;

        _attackStance = false;
        entity.anim.SetBool("meleeAttack", false);
        entity.anim.SetBool("idle", true);

        meleeAttackType++;
        if (meleeAttackType >= attackDetails.Length)
            meleeAttackType = 0;
    }
}
