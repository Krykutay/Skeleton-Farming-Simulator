public class E6_StunState : StunState
{
    readonly Enemy6 enemy;

    public E6_StunState(Enemy6 enemy, FiniteStateMachine stateMachine, string animBoolName, D_StunState stateData)
        : base(enemy, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isGrounded && (yPosBeforeKnockback - entity.transform.position.y) > 3f)
        {
            entity.SetVelocityX(0f);
            enemy.JustDied();
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
            stateMachine.ChangeState(enemy.rangeAttackState);
        }
        else
        {
            enemy.idleState.SetFlipAfterIdle(false);
            stateMachine.ChangeState(enemy.idleState);
        }
    }

}
