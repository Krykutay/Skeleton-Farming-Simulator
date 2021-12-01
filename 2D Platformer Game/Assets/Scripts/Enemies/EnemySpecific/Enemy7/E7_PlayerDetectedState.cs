using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E7_PlayerDetectedState : PlayerDetectedState
{
    Enemy7 enemy;

    public E7_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetectedState stateData, Enemy7 enemy) : base(entity, stateMachine, animBoolName, stateData)
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

        if (performMeleeRangeAction)
        {
            stateMachine.ChangeState(enemy.meleeAttackState);
        }
        else if (!isDetectingLedge)
        {
            entity.Flip();
            stateMachine.ChangeState(enemy.moveState);
        }
        else if (isPlayerInMinAgroRange && entity.CheckIfPlayerReachableByMeleeAction())
        {
            stateMachine.ChangeState(enemy.chargeState);
        }
        else if (performLongRangeAction && isPlayerInMaxAgroRange && entity.CheckIfPlayerReachableByMeleeAction())
        {
            stateMachine.ChangeState(enemy.chargeState);
        }
        else if (!isPlayerInMaxAgroRange)
        {
            enemy.idleState.SetFlipAfterIdle(false);
            stateMachine.ChangeState(enemy.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
