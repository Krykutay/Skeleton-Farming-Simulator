using System;
using UnityEngine;

public class E5_DeadState : DeadState
{
    Enemy5 enemy;

    public E5_DeadState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DeadState stateData, Enemy5 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        TimeOfDeath = Time.time;
        //Died?.Invoke(enemy);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isAnimationFinished)
            return;

        if (Time.time >= TimeOfDeath + stateData.respawnTime)
        {

            stateMachine.ChangeState(enemy.respawnState);


            // TODO: If some other case like it falls from ledge etc, send it to object pool and get it back
            //Enemy4Pool.Instance.ReturnToPool(enemy);
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
}
