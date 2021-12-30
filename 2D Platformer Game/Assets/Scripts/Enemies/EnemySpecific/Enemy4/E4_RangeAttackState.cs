using UnityEngine;

public class E4_RangeAttackState : RangeAttackState
{
    readonly Enemy4 enemy;

    public E4_RangeAttackState(Enemy4 enemy, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_RangeAttackState stateData) 
        : base(enemy, stateMachine, animBoolName, attackPosition, stateData)
    {
        this.enemy = enemy;
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

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        SoundManager.Instance.Play(SoundManager.SoundTags.SkeletonBow);
        projectile = EnemyArrowPool.Instance.Get(attackPosition.position, attackPosition.rotation);
        projectile.FireProjectile(stateData.projectileSpeed, stateData.projectileTravelDistance, stateData.projectileDamage, entity);
    }

}
