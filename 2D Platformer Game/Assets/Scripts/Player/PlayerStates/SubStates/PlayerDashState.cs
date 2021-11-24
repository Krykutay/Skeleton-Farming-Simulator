using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    public bool canDash { get; private set; }

    Vector2 _dashDirection;
    Vector2 _dashDirectionInput;
    Vector2 _lastAfterImagePos;

    float _lastDashTime = Mathf.NegativeInfinity;

    int _xInput;
    int _yInput;

    bool _isGrounded;
    bool _isHolding;
    bool _dashInputStopped;

    public PlayerDashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()    // TODO: use walking animation if on the ground
    {
        base.Enter();

        isAbilityDone = false;

        canDash = false;
        player.inputHandler.UseDashInput();

        _isHolding = true;
        _dashDirection = Vector2.right * player.facingDirection;

        Time.timeScale = playerData.holdTimeScale;
        startTime = Time.unscaledTime;

        player.dashDirectionIndicator.gameObject.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();

        if (player.currentVelocity.y > 0.01f)
        {
            player.SetVelocityY(player.currentVelocity.y * playerData.dashEndYMultiplier);
        }

        if (_xInput != 0)
            player.SetVelocityX(playerData.movementVelocity * _xInput);
        else
            player.SetVelocityX(0f);

        player.anim.SetBool("move", false);
        player.anim.SetBool("idle", false);
        player.anim.SetBool("crouchMove", false);
        player.anim.SetBool("crouchIdle", false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isExitingState)
            return;

        _xInput = player.inputHandler.xInput;
        _yInput = player.inputHandler.yInput;
        player.anim.SetFloat("yVelocity", player.currentVelocity.y);
        player.anim.SetFloat("xVelocity", Mathf.Abs(player.currentVelocity.x));

        if (_isHolding)     // waiting for player direction
        {
            AdjustHoldingAnim();
            _dashDirectionInput = player.inputHandler.rawDashDirectionInput;
            _dashInputStopped = player.inputHandler.dashInputStopped;

            if (_dashDirectionInput != Vector2.zero)
            {
                _dashDirection = _dashDirectionInput;
                _dashDirection.Normalize();
            }

            float angle = Vector2.SignedAngle(Vector2.right, _dashDirection);
            player.dashDirectionIndicator.rotation = Quaternion.Euler(0f, 0f, angle - 45f);     // initial image already has 45 degrees to the +z

            if (_dashInputStopped || Time.unscaledTime >= startTime + playerData.maxHoldTime)
            {
                _isHolding = false;
                Time.timeScale = 1f;
                startTime = Time.time;

                int dashDirectionX = _dashDirection.x > 0f ? 1 : -1;
                player.CheckIfShouldFlip(Mathf.RoundToInt(dashDirectionX));

                player.rb.drag = playerData.drag;
                player.SetVelocity(playerData.dashVelocity, _dashDirection);

                player.dashDirectionIndicator.gameObject.SetActive(false);
                PlaceAfterImage();
            }
        }
        else    // performing the dash Action
        {
            AdjustReleasingAnim();
            player.SetVelocity(playerData.dashVelocity, _dashDirection);
            CheckIfShouldPlaceAfterImage();

            if (Time.time >= startTime + playerData.dashTime)
            {
                player.rb.drag = 0f;
                isAbilityDone = true;
                _lastDashTime = Time.time;
            }
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();

        _isGrounded = player.CheckIfGrounded();
    }


    void PlaceAfterImage()
    {
        PlayerAfterImagePool.Instance.Get();
        _lastAfterImagePos = player.transform.position;
    }

    void CheckIfShouldPlaceAfterImage()
    {
        if (Vector2.Distance(player.transform.position, _lastAfterImagePos) >= playerData.distBetweenAfterImages)
        {
            PlaceAfterImage();
        }
    }

    void AdjustHoldingAnim()
    {
        if (_isGrounded)
        {
            player.anim.SetBool("inAir", false);

            if (_xInput == 0 && _yInput != -1)
                player.anim.SetBool("idle", true);
            else if (_xInput != 0 && _yInput != -1)
                player.anim.SetBool("move", true);
            else if (_xInput == 0 && _yInput == -1)
                player.anim.SetBool("crouchIdle", true);
            else if (_xInput != 0 && _yInput == -1)
                player.anim.SetBool("crouchMove", true);
        }
        else
        {
            player.anim.SetBool("inAir", true);
            player.anim.SetBool("move", false);
            player.anim.SetBool("idle", false);
            player.anim.SetBool("crouchMove", false);
            player.anim.SetBool("crouchIdle", false);
        }
    }

    void AdjustReleasingAnim()
    {
        if (_isGrounded)
        {
            player.anim.SetBool("crouchMove", false);
            player.anim.SetBool("crouchIdle", false);
            player.anim.SetBool("inAir", false);
            player.anim.SetBool("idle", false);
            player.anim.SetBool("move", true);
        }
        else
        {
            player.anim.SetBool("inAir", true);
            player.anim.SetBool("move", false);
            player.anim.SetBool("idle", false);
        }
    }

    public bool CheckIfCanDash() => canDash && Time.time >= _lastDashTime + playerData.dashCooldown;

    public void ResetCanDash() => canDash = true;

}
