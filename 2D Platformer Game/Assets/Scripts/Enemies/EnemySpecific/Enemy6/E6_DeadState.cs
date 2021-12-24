using System;
using UnityEngine;

public class E6_DeadState : DeadState
{
    Enemy6 enemy;

    public E6_DeadState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DeadState stateData, Enemy6 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
            //Enemy6Pool.Instance.ReturnToPool(enemy);
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
