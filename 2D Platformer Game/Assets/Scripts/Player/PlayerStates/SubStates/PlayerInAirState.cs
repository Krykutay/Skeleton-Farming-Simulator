using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    int _xInput;

    bool _isGrounded;
    bool _isTouchingWall;
    bool _jumpInput;
    bool _jumpInputStopped;
    bool _coyoteTime;
    bool _isJumping;
    bool _grabInput;

    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()  // TODO: may change instantly stopping on the air
    {
        base.LogicUpdate();

        CheckCoyoteTime();

        _xInput = player.inputHandler.xInput;
        _jumpInput = player.inputHandler.jumpInput;
        _jumpInputStopped = player.inputHandler.jumpInputStopped;
        _grabInput = player.inputHandler.grabInput;

        CheckJumpMultiplier();

        if (_isGrounded && player.currentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.landState);
        }
        else if (_jumpInput && player.jumpState.CanJump())
        {
            player.inputHandler.UseJumpInput();
            stateMachine.ChangeState(player.jumpState);
        }
        else if (_isTouchingWall && _grabInput)
        {
            stateMachine.ChangeState(player.wallGrabState);
        }
        else if (_isTouchingWall && _xInput == player.facingDirection && player.currentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.wallSlideState);
        }
        else
        {
            player.CheckIfShouldFlip(_xInput);
            player.SetVelocityX(playerData.movementVelocity * _xInput);

            player.anim.SetFloat("yVelocity", player.currentVelocity.y);
            player.anim.SetFloat("xVelocity", Mathf.Abs(player.currentVelocity.x));
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
    }

    void CheckJumpMultiplier()
    {
        if (_isJumping)
        {
            if (_jumpInputStopped)
            {
                player.SetVelocityY(player.currentVelocity.y * playerData.variableJumpHeightMultiplier);
                _isJumping = false;
            }
            else if (player.currentVelocity.y <= 0.01f)
            {
                _isJumping = false;
            }
        }
    }

    void CheckCoyoteTime()
    {
        if (_coyoteTime && Time.time > startTime + playerData.coyoteTime)
        {
            _coyoteTime = false;
            player.jumpState.DecreaseAmountOfJumpsLeft();
        }
    }

    public void StartCoyoteTime()
    {
        _coyoteTime = true;
    }

    public void SetIsJumping()
    {
        _isJumping = true;
    }
}
