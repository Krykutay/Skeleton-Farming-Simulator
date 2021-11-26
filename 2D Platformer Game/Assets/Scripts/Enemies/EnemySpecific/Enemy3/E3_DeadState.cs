using System;
using UnityEngine;

public class E3_DeadState : DeadState
{
    public static Action<Enemy3> Died;

    Enemy3 enemy;

    public E3_DeadState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DeadState stateData, Enemy3 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        TimeOfDeath = Time.time;
        Died?.Invoke(enemy);
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

        if (Time.time >= TimeOfDeath + stateData.corpseDuration)
        {
            //entity.anim.SetBool("dead", false);
            isAnimationFinished = false;

            Enemy3Pool.Instance.ReturnToPool(enemy);
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
