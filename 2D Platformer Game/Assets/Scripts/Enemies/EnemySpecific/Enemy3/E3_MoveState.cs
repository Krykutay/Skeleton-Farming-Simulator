using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_MoveState : MoveState
{
    Enemy3 enemy;

    public E3_MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData, Enemy3 enemy) : base(entity, stateMachine, animBoolName, stateData)
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

        if (isDetectingWall || !isDetectingLedge)
        {
            if (!isPlayerInMaxAgroRange)
            {
                enemy.idleState.SetFlipAfterIdle(true);
                stateMachine.ChangeState(enemy.idleState);
            }
            else
            {
                entity.SetVelocityX(0f);
                stateMachine.ChangeState(enemy.playerDetectedState);
            }
        }
        else if (!canLeaveMoveState)
        {
            return;
        }
        else if (isPlayerInMaxAgroRange)
        {
            enemy.playerDetectedState.PlayDetectionSound();
            stateMachine.ChangeState(enemy.playerDetectedState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
