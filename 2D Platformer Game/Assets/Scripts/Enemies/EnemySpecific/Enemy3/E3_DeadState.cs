using UnityEngine;

public class E3_DeadState : DeadState
{
    readonly Enemy3 enemy;

    public E3_DeadState(Enemy3 enemy, FiniteStateMachine stateMachine, string animBoolName, D_DeadState stateData) 
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
                Enemy3Pool.Instance.ReturnToPool(enemy);
            }
        }
    }

}
