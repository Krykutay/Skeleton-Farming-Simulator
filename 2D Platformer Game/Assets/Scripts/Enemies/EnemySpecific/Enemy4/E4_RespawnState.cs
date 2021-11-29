using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E4_RespawnState : RespawnState
{
    Enemy4 enemy;

    public E4_RespawnState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_RespawnState stateData, Enemy4 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
