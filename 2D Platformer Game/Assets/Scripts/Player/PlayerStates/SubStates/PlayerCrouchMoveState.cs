using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchMoveState : PlayerGroundedState
{
    public PlayerCrouchMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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

        if (isExitingState)
            return;

        player.SetVelocityX(playerData.crouchMovementVelocity * player.facingDirection);
        player.CheckIfShouldFlip(xInput);

        if (xInput == 0)
        {
            stateMachine.ChangeState(player.crouchIdleState);
        }
        else if (yInput != -1 && !isTouchingCeiling)
        {
            stateMachine.ChangeState(player.moveState);
        }

    }
}
