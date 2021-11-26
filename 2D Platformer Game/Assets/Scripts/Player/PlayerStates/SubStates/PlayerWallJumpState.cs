using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    int _wallJumpDirection;

    public float previousWallJumpXPosition { get; private set; } = Mathf.NegativeInfinity;

    public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        previousWallJumpXPosition = player.transform.position.x;

        isAbilityDone = false;
        player.jumpState.ResetAmountOfJumpsLeft();
        player.jumpState.DecreaseAmountOfJumpsLeft();
        player.inputHandler.UseJumpInput();
        player.SetVelocity(playerData.wallJumpVelocity, playerData.wallJumpAngle, _wallJumpDirection);
        player.CheckIfShouldFlip(_wallJumpDirection);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.anim.SetFloat("yVelocity", player.currentVelocity.y);

        if (Time.time >= startTime + playerData.wallJumpTime)
        {
            isAbilityDone = true;
        }
    }

    public void DetermineWallJumpDirection(bool isTouchingWall)
    {
        if (isTouchingWall)
        {
            _wallJumpDirection = -player.facingDirection;
        }
        else
        {
            _wallJumpDirection = player.facingDirection;
        }
    }

    public void ResetPreviousWallJumpXPosition()
    {
        previousWallJumpXPosition = Mathf.NegativeInfinity;
    }

    public bool CheckIfCanWallJump() => Mathf.Abs(player.transform.position.x - previousWallJumpXPosition) > 1f;
}
