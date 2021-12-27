public class PlayerWallClimbState : PlayerTouchingWallState
{
    public PlayerWallClimbState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isExitingState)
            return;

        if(yInput == 0)
        {
            stateMachine.ChangeState(player.wallGrabState);
        }
        else if (!grabInput && xInput == player.facingDirection)
        {
            stateMachine.ChangeState(player.wallSlideState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        player.SetVelocityY(playerData.wallClimbVelocity);
    }
}
