using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{
    public PlayerLandState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        CinemachineShake.Instance.ShakeCamera(1.75f, 0.1f);
        DustJumpParticlePool.Instance.Get(player._groundCheck.position, Quaternion.Euler(-90f, 0f, 0f));
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isExitingState)
            return;

        //player.CheckIfShouldFlip(xInput);

        //player.SetVelocityX(playerData.movementVelocity * xInput);

        //if (!isAnimationFinished)
          //  return;
        
        if (xInput != 0)
        {
            stateMachine.ChangeState(player.moveState);
        }
        else
        {
            stateMachine.ChangeState(player.idleState);
        }
        
    }
}
