using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallGrabState : PlayerTouchingWallState
{
    Vector2 _holdPosition;

    public PlayerWallGrabState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

  

    public override void Enter()
    {
        base.Enter();

        //_holdPosition = player.transform.position;
        //HoldPosition();

        core.movement.SetVelocityX(0f);
        core.movement.SetVelocityY(0f);
        player.rb.gravityScale = 0;
    }

    public override void Exit()
    {
        base.Exit();

        player.rb.gravityScale = player.initialGravity;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //HoldPosition();
        if (isExitingState)
            return;
        
        if (yInput > 0.01f)
        {
            stateMachine.ChangeState(player.wallClimbState);
        }
        else if (!grabInput)
        {
            stateMachine.ChangeState(player.inAirState);
        }
        
    }

    void HoldPosition()     // TODO: try rb gravity = 0
    {
        player.transform.position = _holdPosition;

        core.movement.SetVelocityX(0f);
        core.movement.SetVelocityY(0f);
    }
}
