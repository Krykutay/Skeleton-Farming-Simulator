using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnState : State
{
    protected D_RespawnState stateData;

    protected bool isAnimationFinished;

    public RespawnState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_RespawnState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        SoundManager.Instance.Play(SoundManager.SoundTags.SkeletonRespawn);
        entity.atsm.respawnState = this;
        isAnimationFinished = false;
        entity.SetVelocityX(0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
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
