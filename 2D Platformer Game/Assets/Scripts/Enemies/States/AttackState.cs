using UnityEngine;

public class AttackState : State
{
    protected Transform attackPosition;

    protected bool isAnimationFinished;
    protected bool isPlayerMinAgroRange;
    protected bool isPlayerMaxAgroRange;

    public AttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition) : base(entity, stateMachine, animBoolName)
    {
        this.attackPosition = attackPosition;
    }

    public override void Enter()
    {
        base.Enter();

        entity.atsm.attackState = this;
        isAnimationFinished = false;
        entity.SetVelocityX(0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerMinAgroRange = entity.CheckPlayerInMinAgroRange();
        isPlayerMaxAgroRange = entity.CheckPlayerInMaxAgroRange();
    }

    public virtual void TriggerAttack()
    {

    }

    public virtual void FinishAttack()
    {
        isAnimationFinished = true;
    }

}
