using UnityEngine;

public class E7_DeadState : DeadState
{
    readonly Enemy7 enemy;

    public E7_DeadState(Enemy7 enemy, FiniteStateMachine stateMachine, string animBoolName, D_DeadState stateData)
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
                Entity.OnEnemyDied?.Invoke(enemy);
                enemy.anim.WriteDefaultValues();
                Enemy7Pool.Instance.ReturnToPool(enemy);
            }
        }
    }

}
