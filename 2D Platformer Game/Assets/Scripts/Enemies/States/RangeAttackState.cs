using UnityEngine;

public class RangeAttackState : AttackState
{
    protected D_RangeAttackState stateData;

    protected Projectile projectile;

    protected AttackDetails[] attackDetails;

    protected float rangeAttackCooldown;

    float rangeAttackFinishTime = Mathf.NegativeInfinity;
    bool _attackStance;

    public RangeAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_RangeAttackState stateData)
        : base(entity, stateMachine, animBoolName, attackPosition)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        rangeAttackCooldown = stateData.rangeAttackCooldown;

        _attackStance = false;
        entity.anim.SetBool("rangeAttack", false);
        entity.anim.SetBool("idle", true);
    }

    public override void Exit()
    {
        base.Exit();

        entity.anim.SetBool("rangeAttack", false);
        entity.anim.SetBool("idle", false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= rangeAttackFinishTime + rangeAttackCooldown && !_attackStance)
        {
            _attackStance = true;
            entity.anim.SetBool("idle", false);
            entity.anim.SetBool("rangeAttack", true);
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
    }

    public override void FinishAttack()
    {
        base.FinishAttack();

        rangeAttackFinishTime = Time.time;

        _attackStance = false;
        entity.anim.SetBool("rangeAttack", false);
        entity.anim.SetBool("idle", true);

    }
}
