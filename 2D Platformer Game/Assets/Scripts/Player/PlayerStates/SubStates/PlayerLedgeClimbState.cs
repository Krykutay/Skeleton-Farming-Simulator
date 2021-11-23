using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLedgeClimbState : PlayerState
{
    Vector2 _detectedPos;
    Vector2 _cornerPos;
    Vector2 _startPos;
    Vector2 _stopPos;
    Vector2 _workSpace;

    int _xInput;
    int _yInput;
    bool _jumpInput;

    bool _isHanging;
    bool _isClimbing;
    bool _isTouchingCeiling;

    public PlayerLedgeClimbState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        
        core.movement.SetVelocityZero();
        player.rb.gravityScale = 0;

        player.transform.position = _detectedPos;
        _cornerPos = DetermineCornerPosition();

        _startPos.Set(_cornerPos.x - (core.movement.facingDirection * playerData.startOffset.x), _cornerPos.y - playerData.startOffset.y);
        _stopPos.Set(_cornerPos.x + (core.movement.facingDirection * playerData.stopOffset.x), _cornerPos.y + playerData.stopOffset.y);

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
            if (_isTouchingCeiling)
                stateMachine.ChangeState(player.crouchIdleState);
            else
                stateMachine.ChangeState(player.idleState);
            return;
        }

        _xInput = player.inputHandler.xInput;
        _yInput = player.inputHandler.yInput;
        _jumpInput = player.inputHandler.jumpInput;

        if (_xInput == core.movement.facingDirection && _isHanging && !_isClimbing)
        {
            CheckForSpace();
            _isClimbing = true;
            player.anim.SetBool("climbLedge", true);
        }
        else if (_yInput == -1 && _isHanging && !_isClimbing)
        {
            stateMachine.ChangeState(player.inAirState);
        }
        else if (_jumpInput && !_isClimbing)
        {
            player.wallJumpState.DetermineWallJumpDirection(true);
            stateMachine.ChangeState(player.wallJumpState);
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

    void CheckForSpace()
    {
        _isTouchingCeiling = Physics2D.Raycast(
            _cornerPos + (Vector2.up * 0.015f) + (Vector2.right * core.movement.facingDirection * 0.015f), Vector2.up,
            playerData.standColliderHeight, core.collusionSenses.groundLayer);

        player.anim.SetBool("isTouchingCeiling", _isTouchingCeiling);
    }

    Vector2 DetermineCornerPosition()
    {
        RaycastHit2D xHit = Physics2D.Raycast(
            core.collusionSenses.wallCheck.position,
            Vector2.right * core.movement.facingDirection, core.collusionSenses.wallCheckDistance,
            core.collusionSenses.groundLayer);
        float xDist = xHit.distance;
        _workSpace.Set((xDist + 0.015f) * core.movement.facingDirection, 0f);

        RaycastHit2D yHit = Physics2D.Raycast(
            core.collusionSenses.ledgeCheck.position + (Vector3)_workSpace, Vector2.down,
            core.collusionSenses.ledgeCheck.position.y - core.collusionSenses.wallCheck.position.y + 0.015f,
            core.collusionSenses.groundLayer);
        float yDist = yHit.distance;
        _workSpace.Set(core.collusionSenses.wallCheck.position.x + xDist * core.movement.facingDirection, core.collusionSenses.ledgeCheck.position.y - yDist);

        return _workSpace;
    }

}
