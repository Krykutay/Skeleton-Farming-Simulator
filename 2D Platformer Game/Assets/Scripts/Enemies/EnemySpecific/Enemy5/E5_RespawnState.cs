public class E5_RespawnState : RespawnState
{
    readonly Enemy5 enemy;

    public E5_RespawnState(Enemy5 enemy, FiniteStateMachine stateMachine, string animBoolName, D_RespawnState stateData)
        : base(enemy, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            entity.Respawned();
            stateMachine.ChangeState(enemy.moveState);
        }
    }

}
