using UnityEngine;

public class E6_PlayerDetectedState : PlayerDetectedState
{
    Enemy6 enemy;

    Vector3 _minTeleportLeftLength;
    Vector3 _minTeleportRightLength;

    public E6_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetectedState stateData, Enemy6 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
            if (Time.time >= enemy.teleportState.startTime + enemy.teleportStateData.teleportCooldown)
            {
                TransitionToTeleportState();
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

    void TransitionToTeleportState()
    {
        _minTeleportLeftLength.Set(enemy.transform.position.x - 1.5f * enemy.facingDirection, enemy.transform.position.y, enemy.transform.position.z);
        _minTeleportRightLength.Set(enemy.transform.position.x + 1.5f * enemy.facingDirection, enemy.transform.position.y, enemy.transform.position.z);

        bool canTeleportLeft = enemy.teleportState.CheckCanTeleport(_minTeleportLeftLength);
        bool canTeleportRight = enemy.teleportState.CheckCanTeleport(_minTeleportRightLength);

        float randomAction = Random.Range(0, 2);

        if (randomAction == 0)
        {
            if (canTeleportLeft)
            {
                stateMachine.ChangeState(enemy.teleportState);
            }
            else if (canTeleportRight)
            {
                entity.Flip();
                stateMachine.ChangeState(enemy.teleportState);
            }
            else
            {
                stateMachine.ChangeState(enemy.meleeAttackState);
            }
        }
        else
        {
            if (canTeleportRight)
            {
                entity.Flip();
                stateMachine.ChangeState(enemy.teleportState);
            }
            else if (canTeleportLeft)
            {
                stateMachine.ChangeState(enemy.teleportState);
            }
            else
            {
                stateMachine.ChangeState(enemy.meleeAttackState);
            }
        }
    }
}
