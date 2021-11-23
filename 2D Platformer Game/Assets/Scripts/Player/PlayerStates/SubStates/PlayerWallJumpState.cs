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

        isAbilityDone = false;
        player.jumpState.ResetAmountOfJumpsLeft();
        player.jumpState.DecreaseAmountOfJumpsLeft();
        player.inputHandler.UseJumpInput();
        core.movement.SetVelocity(playerData.wallJumpVelocity, playerData.wallJumpAngle, _wallJumpDirection);
        core.movement.CheckIfShouldFlip(_wallJumpDirection);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.anim.SetFloat("yVelocity", core.movement.currentVelocity.y);
        player.anim.SetFloat("xVelocity", Mathf.Abs(core.movement.currentVelocity.x));

        if (Time.time >= startTime + playerData.wallJumpTime)
        {
            isAbilityDone = true;
        }
    }

    public void DetermineWallJumpDirection(bool isTouchingWall)
    {
        if (isTouchingWall)
        {
            _wallJumpDirection = -core.movement.facingDirection;
        }
        else
        {
            _wallJumpDirection = core.movement.facingDirection;
        }
    }
}
