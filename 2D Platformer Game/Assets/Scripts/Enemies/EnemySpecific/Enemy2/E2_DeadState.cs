using System;
using UnityEngine;

public class E2_DeadState : DeadState
{
    public static Action<Enemy2> Died;

    Enemy2 enemy;

    public E2_DeadState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DeadState stateData, Enemy2 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        DeathChunkParticlePool.Instance.Get(entity.transform.position, Quaternion.Euler(0f, 0f, 0f));
        DeathBloodParticlePool.Instance.Get(entity.transform.position, Quaternion.Euler(0f, 0f, 0f));

        Died?.Invoke(enemy);
        Enemy2Pool.Instance.ReturnToPool(enemy);
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
}
