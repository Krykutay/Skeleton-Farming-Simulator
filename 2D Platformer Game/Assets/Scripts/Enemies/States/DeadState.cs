using UnityEngine;

public class DeadState : State
{
    protected D_DeadState stateData;

    protected float TimeOfDeath;

    protected bool isAnimationFinished;

    public DeadState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DeadState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        entity.atsm.deadState = this;
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
    }

    public virtual void Dead()
    {
        isAnimationFinished = true;
    }
}
