public class E7_RespawnState : RespawnState
{
    readonly Enemy7 enemy;

    public E7_RespawnState(Enemy7 enemy, FiniteStateMachine stateMachine, string animBoolName, D_RespawnState stateData)
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
