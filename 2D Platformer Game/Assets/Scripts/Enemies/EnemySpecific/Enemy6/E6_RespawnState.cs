public class E6_RespawnState : RespawnState
{
    readonly Enemy6 enemy;

    public E6_RespawnState(Enemy6 enemy, FiniteStateMachine stateMachine, string animBoolName, D_RespawnState stateData)
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
