using System;
using UnityEngine;

public class E4_DeadState : DeadState
{
    public static Action<Enemy4> Died;

    Enemy4 enemy;

    public E4_DeadState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DeadState stateData, Enemy4 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        DeathChunkParticlePool.Instance.Get(entity.transform.position, Quaternion.Euler(0f, 0f, 0f));
        DeathBloodParticlePool.Instance.Get(entity.transform.position, Quaternion.Euler(0f, 0f, 0f));

        Died?.Invoke(enemy);
        //Enemy4Pool.Instance.ReturnToPool(enemy);
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
