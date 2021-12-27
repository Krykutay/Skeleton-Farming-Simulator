public class E6_IdleState : IdleState
{
    readonly Enemy6 enemy;

    public E6_IdleState(Enemy6 enemy, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData)
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
            detectionSoundPlayed = true;
            stateMachine.ChangeState(enemy.moveState);
        }
    }

}
