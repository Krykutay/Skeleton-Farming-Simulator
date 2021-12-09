using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E7_MoveState : MoveState
{
    Enemy7 enemy;

    public E7_MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData, Enemy7 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
            enemy.idleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(enemy.idleState);
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
