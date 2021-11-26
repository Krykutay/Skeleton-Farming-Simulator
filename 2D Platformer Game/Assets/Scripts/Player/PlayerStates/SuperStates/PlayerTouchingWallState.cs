using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchingWallState : PlayerState
{
    protected int xInput;
    protected int yInput;

    protected bool isGrounded;
    protected bool isTouchingWall;
    protected bool isTouchingVerticalLedge;
    protected bool grabInput;
    protected bool jumpInput;

    public PlayerTouchingWallState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.inputHandler.xInput;
        yInput = player.inputHandler.yInput;
        grabInput = player.inputHandler.grabInput;
        jumpInput = player.inputHandler.jumpInput;

        if (jumpInput && player.wallJumpState.CheckIfCanWallJump())
        {
            player.wallJumpState.DetermineWallJumpDirection(isTouchingWall);
            stateMachine.ChangeState(player.wallJumpState);
        }
        else if (isGrounded && !grabInput)
        {
            stateMachine.ChangeState(player.idleState);
        }
        else if (!isTouchingWall || (xInput != player.facingDirection && !grabInput))
        {
            stateMachine.ChangeState(player.inAirState);
        }
        else if (isTouchingWall && !isTouchingVerticalLedge)
        {
            stateMachine.ChangeState(player.ledgeClimbState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = player.CheckIfGrounded();
        isTouchingWall = player.CheckIfTouchingWall();
        isTouchingVerticalLedge = player.CheckIfTouchingVerticalLedge();

        if (isTouchingWall && !isTouchingVerticalLedge)
        {
            player.ledgeClimbState.SetDetectedPosition(player.transform.position);
        }
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }
}
