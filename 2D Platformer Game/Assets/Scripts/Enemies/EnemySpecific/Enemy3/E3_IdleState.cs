public class E3_IdleState : IdleState
{
    readonly Enemy3 enemy;

    public E3_IdleState(Enemy3 enemy, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData) 
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
