using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerGroundedState : PlayerState
{
    // Inputs
    protected int xInput;
    protected int yInput;
    bool _jumpInput;
    bool _grabInput;
    bool _dashInput;
    bool _attackInput;
    bool _defenseInput;

    // Checks
    protected bool isTouchingCeiling;
    bool _isGrounded;
    bool _isTouchingWall;
    bool _isTouchingVerticalLedge;

    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.jumpState.ResetAmountOfJumpsLeft();
        player.dashState.ResetCanDash();
        player.wallJumpState.ResetPreviousWallJumpXPosition();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.inputHandler.xInput;
        yInput = player.inputHandler.yInput;
        _jumpInput = player.inputHandler.jumpInput;
        _grabInput = player.inputHandler.grabInput;
        _dashInput = player.inputHandler.dashInput;
        _attackInput = player.inputHandler.attackInput;
        _defenseInput = player.inputHandler.defenseInput;

        if (_attackInput && !isTouchingCeiling && !EventSystem.current.IsPointerOverGameObject())
        {
            stateMachine.ChangeState(player.primaryAttackState);
        }
        else if (_defenseInput && !isTouchingCeiling)
        {
            stateMachine.ChangeState(player.defenseState);
        }
        else if (_jumpInput && player.jumpState.CanJump() && !isTouchingCeiling)
        {
            DustJumpParticlePool.Instance.Get(player._groundCheck.position, Quaternion.Euler(-90f, 0f, 0f));
            stateMachine.ChangeState(player.jumpState);
        }
        else if (!_isGrounded)
        {
            player.inAirState.StartCoyoteTime();
            stateMachine.ChangeState(player.inAirState);
        }
        else if (_isTouchingWall && _grabInput && _isTouchingVerticalLedge)
        {
            stateMachine.ChangeState(player.wallGrabState);
        }
        else if (_dashInput && player.dashState.CheckIfCanDash() && !isTouchingCeiling)
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
        _isTouchingVerticalLedge = player.CheckIfTouchingVerticalLedge();
        isTouchingCeiling = player.CheckForCeiling();
    }
}
