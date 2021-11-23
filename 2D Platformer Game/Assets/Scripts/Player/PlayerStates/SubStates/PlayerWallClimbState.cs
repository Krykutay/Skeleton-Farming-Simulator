using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallClimbState : PlayerTouchingWallState
{
    public PlayerWallClimbState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isExitingState)
            return;

        core.movement.SetVelocityY(playerData.wallClimbVelocity);

        if(yInput == 0)
        {
            stateMachine.ChangeState(player.wallGrabState);
        }
        else if (!grabInput && xInput == core.movement.facingDirection)
        {
            stateMachine.ChangeState(player.wallSlideState);
        }
    }
}
