public class PlayerKnockbackState : PlayerState
{
    bool _isGrounded;
    bool _dashInput;

    public PlayerKnockbackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetKnockbackOver();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        _dashInput = player.inputHandler.dashInput;

        if (_dashInput && player.dashState.CheckIfCanDash())
        {
            stateMachine.ChangeState(player.dashState);
        }
        else if (_isGrounded && player.currentVelocity.y < 0.01f)
        {
            player.SetVelocityX(0f);
            stateMachine.ChangeState(player.landState);
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();

        _isGrounded = player.CheckIfGrounded();
    }
}
