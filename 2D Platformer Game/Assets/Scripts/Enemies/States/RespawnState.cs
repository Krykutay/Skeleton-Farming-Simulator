using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnState : State
{
    protected D_RespawnState stateData;

    protected bool isAnimationFinished;

    float _playSoundDelay = 0.33f;
    bool _hasSoundStarted;

    public RespawnState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_RespawnState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        entity.atsm.respawnState = this;
        isAnimationFinished = false;
        entity.SetVelocityX(0f);
        _hasSoundStarted = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + _playSoundDelay && !_hasSoundStarted)
        {
            _hasSoundStarted = true;
            SoundManager.Instance.Play(SoundManager.SoundTags.SkeletonRespawn);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public virtual void Respawned()
    {
        isAnimationFinished = true;
    }
}
