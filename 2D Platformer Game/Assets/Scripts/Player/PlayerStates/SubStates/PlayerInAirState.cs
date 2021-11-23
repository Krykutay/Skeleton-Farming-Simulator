using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    // Input
    int _xInput;
    bool _jumpInput;
    bool _jumpInputStopped;
    bool _grabInput;
    bool _dashInput;

    // Checks
    bool _isGrounded;
    bool _isTouchingWall;
    bool _isTouchingWallBack;
    bool _previousIsTouchingWall;
    bool _previousIsTouchingWallBack;
    bool _isJumping;
    bool _isTouchingLedge;

    float _startWallJumpCoyoteTime;
    bool _coyoteTime;
    bool _wallJumpCoyoteTime;

    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _coyoteTime = false;
        _wallJumpCoyoteTime = false;
    }

    public override void Exit()
    {
        base.Exit();

        _previousIsTouchingWall = false;
        _previousIsTouchingWallBack = false;
        _isTouchingWall = false;
        _isTouchingWallBack = false;
    }

    public override void LogicUpdate()  // TODO: may change instantly stopping on the air
    {
        base.LogicUpdate();

        CheckCoyoteTime();
        CheckWallJumpCoyoteTime();

        _xInput = player.inputHandler.xInput;
        _jumpInput = player.inputHandler.jumpInput;
        _jumpInputStopped = player.inputHandler.jumpInputStopped;
        _grabInput = player.inputHandler.grabInput;
        _dashInput = player.inputHandler.dashInput;

        CheckJumpMultiplier();

        if (player.inputHandler.attackInputs[(int)CombatInputs.primary])
        {
            stateMachine.ChangeState(player.primaryAttackState);
        }
        else if (player.inputHandler.attackInputs[(int)CombatInputs.secondary])
        {
            stateMachine.ChangeState(player.secondaryAttackState);
        }
        else if (_isGrounded && core.movement.currentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.landState);
        }
        else if (_isTouchingWall && !_isTouchingLedge && !_isGrounded)
        {
            stateMachine.ChangeState(player.ledgeClimbState);
        }
        else if (_jumpInput && (_isTouchingWall || _isTouchingWallBack || _wallJumpCoyoteTime))
        {
            _wallJumpCoyoteTime = false;
            _isTouchingWall = core.collusionSenses.wallFront;
            player.wallJumpState.DetermineWallJumpDirection(_isTouchingWall);
            stateMachine.ChangeState(player.wallJumpState);
        }
        else if (_jumpInput && player.jumpState.CanJump())
        {
            _coyoteTime = false;
            player.inputHandler.UseJumpInput();
            stateMachine.ChangeState(player.jumpState);
        }
        else if (_isTouchingWall && _grabInput && _isTouchingLedge)
        {
            stateMachine.ChangeState(player.wallGrabState);
        }
        else if (_isTouchingWall && _xInput == core.movement.facingDirection && core.movement.currentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.wallSlideState);
        }
        else if (_dashInput && player.dashState.CheckIfCanDash())
        {
            stateMachine.ChangeState(player.dashState);
        }
        else
        {
            core.movement.CheckIfShouldFlip(_xInput);
            core.movement.SetVelocityX(playerData.movementVelocity * _xInput);

            player.anim.SetFloat("yVelocity", core.movement.currentVelocity.y);
            player.anim.SetFloat("xVelocity", Mathf.Abs(core.movement.currentVelocity.x));
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();

        _previousIsTouchingWall = _isTouchingWall;
        _previousIsTouchingWallBack = _isTouchingWallBack;

        _isGrounded = core.collusionSenses.ground;
        _isTouchingWall = core.collusionSenses.wallFront;
        _isTouchingWallBack = core.collusionSenses.wallBack;
        _isTouchingLedge = player.core.collusionSenses.ledge;

        if (_isTouchingWall && !_isTouchingLedge)
        {
            player.ledgeClimbState.SetDetectedPosition(player.transform.position);
        }

        if (!_wallJumpCoyoteTime && !_isTouchingWall && !_isTouchingWallBack && (_previousIsTouchingWall || _previousIsTouchingWallBack))
        {
            StartWallJumpCoyoteTime();
        }
    }

    void CheckJumpMultiplier()
    {
        if (!_isJumping)
            return;
        
        if (_jumpInputStopped)
        {
            core.movement.SetVelocityY(core.movement.currentVelocity.y * playerData.variableJumpHeightMultiplier);
            _isJumping = false;
        }
        else if (core.movement.currentVelocity.y <= 0.01f)
        {
            _isJumping = false;
        }
        
    }

    void CheckCoyoteTime()
    {
        if (_coyoteTime && Time.time >= startTime + playerData.coyoteTime)
        {
            _coyoteTime = false;
            player.jumpState.DecreaseAmountOfJumpsLeft();
        }
    }

    void CheckWallJumpCoyoteTime()
    {
        if (_wallJumpCoyoteTime && Time.time >= _startWallJumpCoyoteTime + playerData.coyoteTime)
        {
            _wallJumpCoyoteTime = false;
            player.jumpState.DecreaseAmountOfJumpsLeft();
        }
    }

    public void StartCoyoteTime()
    {
        _coyoteTime = true;
    }

    public void StartWallJumpCoyoteTime()
    {
        _wallJumpCoyoteTime = true;
        _startWallJumpCoyoteTime = Time.time;
    }

    public void StoptWallJumpCoyoteTime()
    {
        _wallJumpCoyoteTime = false;
    }

    public void SetIsJumping()
    {
        _isJumping = true;
    }
}
