using UnityEngine;

public class E6_RespawnState : RespawnState
{
    Enemy6 enemy;

    public E6_RespawnState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_RespawnState stateData, Enemy6 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
