public class E4_DodgeState : DodgeState
{
    readonly Enemy4 enemy;

    public E4_DodgeState(Enemy4 enemy, FiniteStateMachine stateMachine, string animBoolName, D_DodgeState stateData) 
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

        enemy.anim.SetFloat("yVelocity", enemy.rb.velocity.y);

        if (!isDodgeOver)
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
