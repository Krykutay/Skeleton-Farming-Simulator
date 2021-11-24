using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState : PlayerState
{
    protected bool isAbilityDone;

    bool _isGrounded;

    public PlayerAbilityState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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

        if (!isAbilityDone)
            return;
        
        if (_isGrounded && player.currentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.idleState);
        }
        else
        {
            stateMachine.ChangeState(player.inAirState);
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();

<<<<<<< HEAD
        _isGrounded = core.collisionSenses.ground;
=======
        _isGrounded = player.CheckIfGrounded();
>>>>>>> Goin-back
    }
}
