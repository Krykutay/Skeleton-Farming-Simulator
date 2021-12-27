using System;
using UnityEngine;

public class E7_DeadState : DeadState
{
    Enemy7 enemy;

    public E7_DeadState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DeadState stateData, Enemy7 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        timeOfDeath = Time.time;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isAnimationFinished)
            return;

        if (Time.time >= timeOfDeath + stateData.respawnTime)
        {
            if (enemy.deathCount == 1)
                stateMachine.ChangeState(enemy.respawnState);
            else
            {
                Entity.Died?.Invoke(enemy);
                enemy.anim.WriteDefaultValues();
                Enemy7Pool.Instance.ReturnToPool(enemy);
            }
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
}
