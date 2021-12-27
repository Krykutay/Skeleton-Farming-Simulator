using UnityEngine;

public class E6_RangeAttackState : RangeAttackState
{
    readonly Enemy6 enemy;

    bool _attackStarted;
    bool _attackTriggered;

    public E6_RangeAttackState(Enemy6 enemy, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_RangeAttackState stateData)
        : base(enemy, stateMachine, animBoolName, attackPosition, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        _attackTriggered = false;
    }

    public override void Exit()
    {
        base.Exit();

        entity.ResetBodyPosition();

        if (_attackStarted && !_attackTriggered)
            EnemySkillPool.Instance.ReturnToPool((ProjectileSkill)projectile);
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

    public override void StartAttack()
    {
        base.StartAttack();

        projectile = EnemySkillPool.Instance.Get(attackPosition.position, attackPosition.rotation);
        _attackStarted = true;
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        SoundManager.Instance.Play(SoundManager.SoundTags.SkeletonSpell);
        projectile.FireProjectile(stateData.projectileSpeed, stateData.projectileTravelDistance, stateData.projectileDamage, entity);
        _attackTriggered = true;
    }

}
