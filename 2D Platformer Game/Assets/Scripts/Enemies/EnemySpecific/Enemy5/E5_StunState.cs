using UnityEngine;

public class E5_StunState : StunState
{
    Enemy5 enemy;

    public E5_StunState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_StunState stateData, Enemy5 enemy) : base(entity, stateMachine, animBoolName, stateData)
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

        if (isGrounded && (yPosBeforeKnockback - entity.transform.position.y) > 3f)
        {
            entity.SetVelocityX(0f);
            enemy.JustDied();
            entity.DropLootOnDeath();
            return;
        }

        if (!isStunDurationOver)
            return;

        if (performedMeleeRangeAction)
        {
            stateMachine.ChangeState(enemy.meleeAttackState);
        }
        else if (isPlayerInMaxAgroRange)
        {
            stateMachine.ChangeState(enemy.chargeState);
        }
        else
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
}
