using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLedgeClimbState : PlayerState
{
    Vector2 _detectedPos;
    Vector2 _cornerPos;
    Vector2 _startPos;
    Vector2 _stopPos;

    int _xInput;
    int _yInput;

    bool _isHanging;
    bool _isClimbing;

    public PlayerLedgeClimbState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocityZero();
        player.rb.gravityScale = 0;

        player.transform.position = _detectedPos;
        _cornerPos = player.DetermineCornerPosition();

        _startPos.Set(_cornerPos.x - (player.facingDirection * playerData.startOffset.x), _cornerPos.y - playerData.startOffset.y);
        _stopPos.Set(_cornerPos.x + (player.facingDirection * playerData.stopOffset.x), _cornerPos.y + playerData.stopOffset.y);

        player.transform.position = _startPos;
    }

    public override void Exit()
    {
        base.Exit();

        _isHanging = false;

        if (_isClimbing)
        {
            player.transform.position = _stopPos;
            _isClimbing = false;
        }

        player.rb.gravityScale = player.initialGravity;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            stateMachine.ChangeState(player.idleState);
            return;
        }

        _xInput = player.inputHandler.xInput;
        _yInput = player.inputHandler.yInput;

        if (_xInput == player.facingDirection && _isHanging && !_isClimbing)
        {
            _isClimbing = true;
            player.anim.SetBool("climbLedge", true);
        }
        else if (_yInput == -1 && _isHanging && !_isClimbing)
        {
            stateMachine.ChangeState(player.inAirState);
        }
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        _isHanging = true;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        player.anim.SetBool("climbLedge", false);
    }

    public void SetDetectedPosition(Vector2 pos)
    {
        _detectedPos = pos;
    }

}
