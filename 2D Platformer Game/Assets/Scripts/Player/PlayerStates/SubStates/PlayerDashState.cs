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

    bool _isHolding;
    bool _dashInputStopped;

    public PlayerDashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

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
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isExitingState)
            return;

        player.anim.SetFloat("yVelocity", player.currentVelocity.y);
        player.anim.SetFloat("xVelocity", Mathf.Abs(player.currentVelocity.x));

        if (_isHolding)     // waiting for player direction
        {
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

    public bool CheckIfCanDash() => canDash && Time.time >= _lastDashTime + playerData.dashCooldown;

    public void ResetCanDash() => canDash = true;


}
