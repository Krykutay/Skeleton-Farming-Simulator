using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInAirState : PlayerState
{
    // Input
    int _xInput;
    bool _jumpInput;
    bool _jumpInputStopped;
    bool _grabInput;
    bool _dashInput;
    bool _attackInput;
    bool _defenseInput;

    // Checks
    bool _isGrounded;
    bool _isTouchingWall;
    bool _isTouchingWallBack;
    bool _previousIsTouchingWall;
    bool _previousIsTouchingWallBack;
    bool _isJumping;
    bool _isTouchingVerticalLedge;

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

        player.anim.SetBool("isGrounded", false);
        _previousIsTouchingWall = false;
        _previousIsTouchingWallBack = false;
        _isTouchingWall = false;
        _isTouchingWallBack = false;
        player.anim.SetFloat("yVelocity", 0f);
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
        _attackInput = player.inputHandler.attackInput;
        _defenseInput = player.inputHandler.defenseInput;

        CheckJumpMultiplier();

        if (_attackInput)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
                stateMachine.ChangeState(player.primaryAttackState);
        }
        else if (_defenseInput)
        {
            stateMachine.ChangeState(player.defenseState);
        }
        else if (_isGrounded && player.currentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.landState);
        }
        else if (_isTouchingWall && !_isTouchingVerticalLedge && !_isGrounded)
        {
            stateMachine.ChangeState(player.ledgeClimbState);
        }
        else if (_jumpInput && (_isTouchingWall || _isTouchingWallBack || _wallJumpCoyoteTime) && player.wallJumpState.CheckIfCanWallJump())
        {
            DustJumpParticlePool.Instance.Get(player._groundCheck.position, Quaternion.Euler(0f, 0f, 0f));
            _wallJumpCoyoteTime = false;

            _isTouchingWall = player.CheckIfTouchingWall();

            player.wallJumpState.DetermineWallJumpDirection(_isTouchingWall);
            stateMachine.ChangeState(player.wallJumpState);
        }
        else if (_jumpInput && player.jumpState.CanJump())
        {
            player.anim.SetTrigger("jump");
            _coyoteTime = false;
            player.inputHandler.UseJumpInput();
            stateMachine.ChangeState(player.jumpState);
        }
        else if (_isTouchingWall && _grabInput && _isTouchingVerticalLedge)
        {
            stateMachine.ChangeState(player.wallGrabState);
        }
        else if (_isTouchingWall && _xInput == player.facingDirection && player.currentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.wallSlideState);
        }
        else if (_dashInput && player.dashState.CheckIfCanDash())
        {
            stateMachine.ChangeState(player.dashState);
        }
        else
        {
            player.CheckIfShouldFlip(_xInput);
            player.SetVelocityX(playerData.movementVelocity * _xInput);

            player.anim.SetFloat("yVelocity", player.currentVelocity.y);
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

        _isGrounded = player.CheckIfGrounded();
        _isTouchingWall = player.CheckIfTouchingWall();
        _isTouchingWallBack = player.CheckIfTouchingWallBack();
        _isTouchingVerticalLedge = player.CheckIfTouchingVerticalLedge();

        if (_isTouchingWall && !_isTouchingVerticalLedge)
        {
            player.ledgeClimbState.SetDetectedPosition(player.transform.position);
        }

        if (!_wallJumpCoyoteTime && !_isTouchingWall && !_isTouchingWallBack && (_previousIsTouchingWall || _previousIsTouchingWallBack) && player.wallJumpState.CheckIfCanWallJump())
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
            player.SetVelocityY(player.currentVelocity.y * playerData.variableJumpHeightMultiplier);
            _isJumping = false;
        }
        else if (player.currentVelocity.y <= 0.01f)
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
