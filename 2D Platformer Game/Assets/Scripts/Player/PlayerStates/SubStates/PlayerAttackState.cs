using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    Weapon _weapon;

    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        isAbilityDone = false;
        _weapon.EnterWeapon();
    }

    public override void Exit()
    {
        base.Exit();

        _weapon.ExitWeapon();
    }

    public void SetWeapon(Weapon weapon)
    {
        this._weapon = weapon;
        weapon.InitializeWeapon(this);
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        isAbilityDone = true;
    }
}
