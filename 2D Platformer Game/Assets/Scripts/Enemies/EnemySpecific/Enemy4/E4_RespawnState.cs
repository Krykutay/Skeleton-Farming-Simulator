public class E4_RespawnState : RespawnState
{
    readonly Enemy4 enemy;

    public E4_RespawnState(Enemy4 enemy, FiniteStateMachine stateMachine, string animBoolName, D_RespawnState stateData) 
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
