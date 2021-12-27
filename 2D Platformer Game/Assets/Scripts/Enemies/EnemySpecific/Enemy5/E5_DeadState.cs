using UnityEngine;

public class E5_DeadState : DeadState
{
    readonly Enemy5 enemy;

    public E5_DeadState(Enemy5 enemy, FiniteStateMachine stateMachine, string animBoolName, D_DeadState stateData)
        : base(enemy, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        timeOfDeath = Time.time;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isAnimationFinished)
            return;

        if (Time.time >= timeOfDeath + stateData.respawnTime)
        {
            if (enemy.deathCount == 1)
                stateMachine.ChangeState(enemy.respawnState);
            else
            {
                Entity.Died?.Invoke(enemy);
                enemy.anim.WriteDefaultValues();
                Enemy5Pool.Instance.ReturnToPool(enemy);
            }
        }
    }

}
