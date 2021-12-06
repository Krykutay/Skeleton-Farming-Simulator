using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    Weapon _weapon;

    int _xInput;
    bool _jumpInput;
    bool _defenseInput;

    bool _isTouchingCeiling;

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

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        _xInput = player.inputHandler.xInput;
        _jumpInput = player.inputHandler.jumpInput;
        _defenseInput = player.inputHandler.defenseInput;

        player.CheckIfShouldFlip(_xInput);
        player.SetVelocityX(_weapon.weaponData.movementSpeed * _xInput);

        if (_jumpInput && player.jumpState.CanJump() && !_isTouchingCeiling)
        {
            _weapon.AnimationCancelled();
            stateMachine.ChangeState(player.jumpState);
        }
        else if (_defenseInput)
        {
            _weapon.AnimationCancelled();
            stateMachine.ChangeState(player.defenseState);
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();

        _isTouchingCeiling = player.CheckForCeiling();
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
