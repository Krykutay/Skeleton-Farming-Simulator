public class PlayerWallSlideState : PlayerTouchingWallState
{
    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        SoundManager.Instance.Play(SoundManager.SoundTags.PlayerWallSlide);
    }

    public override void Exit()
    {
        base.Exit();

        SoundManager.Instance.Stop(SoundManager.SoundTags.PlayerWallSlide);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isExitingState)
            return;

        if (grabInput && yInput == 0)
        {
            stateMachine.ChangeState(player.wallGrabState);
        }
        else if (grabInput && yInput > 0.01f)
        {
            stateMachine.ChangeState(player.wallClimbState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        player.SetVelocityY(-playerData.wallSlideVelocity);
    }
}
