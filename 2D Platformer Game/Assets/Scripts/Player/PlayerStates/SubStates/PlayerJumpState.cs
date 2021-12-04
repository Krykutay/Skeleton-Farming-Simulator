using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    int _amountOfJumpsLeft;

    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        _amountOfJumpsLeft = playerData.amountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();

        player.anim.SetBool("isGrounded", player.CheckIfGrounded());

        player.inputHandler.UseJumpInput();
        player.SetVelocityY(playerData.jumpVelocity);
        _amountOfJumpsLeft--;
        isAbilityDone = true;
        player.inAirState.SetIsJumping();
    }

    public bool CanJump() => _amountOfJumpsLeft > 0;

    public void ResetAmountOfJumpsLeft() => _amountOfJumpsLeft = playerData.amountOfJumps;

    public void DecreaseAmountOfJumpsLeft() => _amountOfJumpsLeft--;
}
