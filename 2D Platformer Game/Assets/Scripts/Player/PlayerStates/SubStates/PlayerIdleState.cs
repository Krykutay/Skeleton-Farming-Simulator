public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocityX(0f);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isExitingState)
            return;

        player.SetVelocityX(0f);

        if (xInput != 0)
        {
            stateMachine.ChangeState(player.moveState);
        }
        else if (yInput == -1)
        {
            stateMachine.ChangeState(player.crouchIdleState);
        }
    }

}
