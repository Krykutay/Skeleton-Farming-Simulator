using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    // Inputs
    protected int xInput;
    bool _jumpInput;
    bool _grabInput;
    bool _dashInput;

    // Checks
    bool _isGrounded;
    bool _isTouchingWall;
    bool _isTouchingLedge;

    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.jumpState.ResetAmountOfJumpsLeft();
        player.dashState.ResetCanDash();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.inputHandler.xInput;
        _jumpInput = player.inputHandler.jumpInput;
        _grabInput = player.inputHandler.grabInput;
        _dashInput = player.inputHandler.dashInput;

        if (_jumpInput && player.jumpState.CanJump())
        {
            stateMachine.ChangeState(player.jumpState);
        }
        else if (!_isGrounded)
        {
            player.inAirState.StartCoyoteTime();
            stateMachine.ChangeState(player.inAirState);
        }
        else if (_isTouchingWall && _grabInput && _isTouchingLedge)
        {
            stateMachine.ChangeState(player.wallGrabState);
        }
        else if (_dashInput && player.dashState.CheckIfCanDash())
        {
            stateMachine.ChangeState(player.dashState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();

        _isGrounded = player.CheckIfGrounded();
        _isTouchingWall = player.CheckIfTouchingWall();
        _isTouchingLedge = player.CheckIfTouchingLedge();
    }
}
