using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeState : State
{
    protected D_ChargeState stateData;

    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;
    protected bool isDetectingLedge;
    protected bool isDetectingWall;
    protected bool isChargeTimeOver;
    protected bool performMeleeRangeAction;

    protected bool canLeaveChargeState { get; private set; }

    float _chargeStateEnterTime;

    public ChargeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_ChargeState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        entity.CheckIfShouldFlip();
        canLeaveChargeState = false;
        _chargeStateEnterTime = Time.time;

        isChargeTimeOver = false;

        if (entity.CheckLedge())
            entity.SetVelocityX(stateData.chargeSpeed);
        else
            entity.SetVelocityX(0f);
    }

    public override void Exit()
    {
        base.Exit();

        entity.ResetBodyPosition();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= _chargeStateEnterTime + stateData.chargeStateDelay)
        {
            canLeaveChargeState = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        entity.RotateBodyToPlayer();

        if (Time.time >= startTime + stateData.chargeTime)
        {
            isChargeTimeOver = true;
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();
        isDetectingLedge = entity.CheckLedge();
        isDetectingWall = entity.CheckWall();
        performMeleeRangeAction = entity.CheckPlayerInMeleeRangeAction();
    }
}
