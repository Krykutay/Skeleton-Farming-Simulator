public class E6_TeleportState : TeleportState
{
    readonly Enemy6 enemy;

    public E6_TeleportState(Enemy6 enemy, FiniteStateMachine stateMachine, string animBoolName, D_TeleportState stateData)
        : base(enemy, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Exit()
    {
        base.Exit();

        entity.SetVelocityX(0f);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        entity.SetVelocityY(0f);

        if (!isTeleportOver)
            return;

        if (isPlayerInMaxAgroRange && performedCloseRangeAction)
        {
            stateMachine.ChangeState(enemy.meleeAttackState);
        }
        else if (isPlayerInMaxAgroRange && !performedCloseRangeAction)
        {
            stateMachine.ChangeState(enemy.rangeAttackState);
        }
        else if (!isPlayerInMaxAgroRange)
        {
            enemy.idleState.SetFlipAfterIdle(false);
            stateMachine.ChangeState(enemy.idleState);
        }
        
    }

}
