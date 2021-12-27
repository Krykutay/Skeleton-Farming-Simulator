public class E3_RespawnState : RespawnState
{
    readonly Enemy3 enemy;

    public E3_RespawnState(Enemy3 enemy, FiniteStateMachine stateMachine, string animBoolName, D_RespawnState stateData) : 
        base(enemy, stateMachine, animBoolName, stateData)
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
