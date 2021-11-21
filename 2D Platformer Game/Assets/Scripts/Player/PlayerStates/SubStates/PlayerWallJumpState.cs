using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    int _wallJumpDirection;

    public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.inputHandler.UseJumpInput();
        isAbilityDone = false;
        player.jumpState.ResetAmountOfJumpsLeft();
        player.SetVelocity(playerData.wallJumpVelocity, playerData.wallJumpAngle, _wallJumpDirection);
        player.CheckIfShouldFlip(_wallJumpDirection);
        player.jumpState.DecreaseAmountOfJumpsLeft();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.anim.SetFloat("yVelocity", player.currentVelocity.y);
        player.anim.SetFloat("xVelocity", Mathf.Abs(player.currentVelocity.x));

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
}
