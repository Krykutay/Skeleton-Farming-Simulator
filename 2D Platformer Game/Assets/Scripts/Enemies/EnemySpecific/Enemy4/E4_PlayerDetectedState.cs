using UnityEngine;

public class E4_PlayerDetectedState : PlayerDetectedState
{
    Enemy4 enemy;

    Vector3 _minDodgeLeftLength;
    Vector3 _minDodgeRightLength;

    public E4_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetectedState stateData, Enemy4 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
            if (Time.time >= enemy.dodgeState.startTime + enemy.dodgeStateData.dodgeCooldown)
            {
                TransitionToDodgeState();
            }
            else
            {
                stateMachine.ChangeState(enemy.meleeAttackState);
            }
        }
        else if (isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(enemy.rangeAttackState);
        }
        else if (performLongRangeAction && isPlayerInMaxAgroRange)
        {
            stateMachine.ChangeState(enemy.rangeAttackState);
        }
        else if (!isDetectingLedge)
        {
            entity.Flip();
            stateMachine.ChangeState(enemy.moveState);
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

    public override void DoChecks()
    {
        base.DoChecks();
    }

    void TransitionToDodgeState()
    {
        _minDodgeLeftLength.Set(enemy.transform.position.x - 1.5f * enemy.facingDirection, enemy.transform.position.y, enemy.transform.position.z);
        _minDodgeRightLength.Set(enemy.transform.position.x + 1.5f * enemy.facingDirection, enemy.transform.position.y, enemy.transform.position.z);

        if (enemy.dodgeState.CheckCanDodge(_minDodgeLeftLength))
        {
            stateMachine.ChangeState(enemy.dodgeState);
        }
        else if (enemy.dodgeState.CheckCanDodge(_minDodgeRightLength))
        {
            entity.Flip();
            stateMachine.ChangeState(enemy.dodgeState);
        }
        else
        {
            stateMachine.ChangeState(enemy.meleeAttackState);
        }
    }
}
