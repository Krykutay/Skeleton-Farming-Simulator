using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectedState : State
{
    protected D_PlayerDetectedState stateData;

    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;
    protected bool performLongRangeAction;
    protected bool performMeleeRangeAction;
    protected bool isDetectingLedge;

    int _randInt;

    public PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetectedState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        _randInt = Random.Range(0, 2);
        if (_randInt == 0)
            SoundManager.Instance.Play(SoundManager.SoundTags.SkeletonDetection1);
        else
            SoundManager.Instance.Play(SoundManager.SoundTags.SkeletonDetection2);

        performLongRangeAction = false;
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

        if (Time.time >= startTime + stateData.LongRangeActionTime)
        {
            performLongRangeAction = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        entity.RotateBodyToPlayer();
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();
        performMeleeRangeAction = entity.CheckPlayerInMeleeRangeAction();
        isDetectingLedge = entity.CheckLedge();
    }
}
