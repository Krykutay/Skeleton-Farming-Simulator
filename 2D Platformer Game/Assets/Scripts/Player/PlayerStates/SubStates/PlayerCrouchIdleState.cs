public class PlayerCrouchIdleState : PlayerGroundedState
{
    public PlayerCrouchIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocityZero();
        player.SetColliderHeight(playerData.crouchColliderHeight);
        player.SetPlayerHitPositionInCrouch();
    }

    public override void Exit()
    {
        base.Exit();

        player.SetColliderHeight(playerData.standColliderHeight);
        player.ResetPlayerHitPosition();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isExitingState)
            return;

        if (xInput != 0)
        {
            stateMachine.ChangeState(player.crouchMoveState);
        }
        else if (yInput != -1 && !isTouchingCeiling)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
