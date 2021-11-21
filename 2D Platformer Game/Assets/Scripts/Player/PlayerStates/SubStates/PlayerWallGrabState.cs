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

        player.SetVelocityX(0f);
        player.SetVelocityY(0f);
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

        if (yInput > 0.01f)
        {
            stateMachine.ChangeState(player.wallClimbState);
        }
        else if (yInput < -0.01f || !grabInput)
        {
            stateMachine.ChangeState(player.wallSlideState);
        }
    }

    void HoldPosition()     // TODO: try rb gravity = 0
    {
        player.transform.position = _holdPosition;

        player.SetVelocityX(0f);
        player.SetVelocityY(0f);
    }
}
