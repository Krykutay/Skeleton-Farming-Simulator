public class E5_IdleState : IdleState
{
    readonly Enemy5 enemy;

    public E5_IdleState(Enemy5 enemy, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData)
        : base(enemy, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isPlayerInMaxAgroRange)
        {
            if (!detectionSoundPlayed)
            {
                enemy.playerDetectedState.PlayDetectionSound();
                detectionSoundPlayed = true;
            }
            
            stateMachine.ChangeState(enemy.playerDetectedState);
        }
        else if (isIdleTimeOver)
        {
            detectionSoundPlayed = false;
            stateMachine.ChangeState(enemy.moveState);
        }
    }

}
