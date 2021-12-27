public class E6_MoveState : MoveState
{
    readonly Enemy6 enemy;

    public E6_MoveState(Enemy6 enemy, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData) 
        : base(enemy, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
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

}
