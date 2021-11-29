using UnityEngine;

public class E3_RespawnState : RespawnState
{
    Enemy3 enemy;

    public E3_RespawnState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_RespawnState stateData, Enemy3 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            entity.Respawned();
            stateMachine.ChangeState(enemy.moveState);
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

    public override void Respawned()
    {
        base.Respawned();
    }
}
