using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDefenseMoveState : PlayerState
{
    int _xInput;
    bool _jumpInput;
    bool _defenseInput;

    bool _isGrounded;
    bool _isTouchingCeiling;
    bool _justGrounded;

    public PlayerDefenseMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _isGrounded = player.CheckIfGrounded();
        _justGrounded = _isGrounded;

        if (_isGrounded && player.currentVelocity.y < 0.01f)
        {
            player.jumpState.ResetAmountOfJumpsLeft();
            player.dashState.ResetCanDash();
            player.wallJumpState.ResetPreviousWallJumpXPosition();
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        _xInput = player.inputHandler.xInput;
        _jumpInput = player.inputHandler.jumpInput;
        _defenseInput = player.inputHandler.defenseInput;

        player.SetVelocityX(playerData.parryMovementVelocity * _xInput);

        _isGrounded = player.CheckIfGrounded();
        if (!_justGrounded && _isGrounded && player.currentVelocity.y < -0.01f)
        {
            DustJumpParticlePool.Instance.Get(player._groundCheck.position, Quaternion.Euler(-90f, 0f, 0f));
            _justGrounded = true;
            player.jumpState.ResetAmountOfJumpsLeft();
            player.dashState.ResetCanDash();
            player.wallJumpState.ResetPreviousWallJumpXPosition();
        }

        if (_jumpInput && player.jumpState.CanJump() && !_isTouchingCeiling)
        {
            if (_isGrounded)
                DustJumpParticlePool.Instance.Get(player._groundCheck.position, Quaternion.Euler(-90f, 0f, 0f));
            player.anim.SetBool("parryStarted", false);
            player.inputHandler.UseJumpInput();
            stateMachine.ChangeState(player.jumpState);
        }
        else if (_xInput == 0)
        {
            player.anim.SetBool("parryStarted", true);
            stateMachine.ChangeState(player.defenseState);
        }
        else if (!_defenseInput && _isGrounded)
        {
            player.anim.SetBool("parryStarted", false);
            stateMachine.ChangeState(player.moveState);
        }
        else if (!_defenseInput && !_isGrounded)
        {
            player.anim.SetBool("parryStarted", false);
            stateMachine.ChangeState(player.inAirState);
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();

        _isTouchingCeiling = player.CheckForCeiling();
    }

}
