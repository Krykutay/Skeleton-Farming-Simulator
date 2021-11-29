using UnityEngine;

public class E4_RangeAttackState : RangeAttackState
{
    Enemy4 enemy;

    public E4_RangeAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_RangeAttackState stateData, Enemy4 enemy)
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
            stateMachine.ChangeState(enemy.lookForPlayerState);
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
