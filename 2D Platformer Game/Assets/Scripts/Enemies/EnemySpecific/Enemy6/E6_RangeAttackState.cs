using UnityEngine;

public class E6_RangeAttackState : RangeAttackState
{
    Enemy6 enemy;

    public E6_RangeAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_RangeAttackState stateData, Enemy6 enemy)
        : base(entity, stateMachine, animBoolName, attackPosition, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        entity.ResetBodyPosition();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isAnimationFinished)
            return;

        if (isPlayerMinAgroRange)
        {
            stateMachine.ChangeState(enemy.playerDetectedState);
        }
        else if (!isPlayerMaxAgroRange)
        {
            enemy.idleState.SetFlipAfterIdle(false);
            stateMachine.ChangeState(enemy.idleState);
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        entity.RotateBodyToPlayer();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        projectile = EnemyArrowPool.Instance.Get(attackPosition.position, attackPosition.rotation);
        projectile.FireProjectile(stateData.projectileSpeed, stateData.projectileTravelDistance, stateData.projectileDamage);
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }
}
