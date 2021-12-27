using UnityEngine;

public class E7_MeleeAttackState : MeleeAttackState
{
    readonly Enemy7 enemy;

    public E7_MeleeAttackState(Enemy7 enemy, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_MeleeAttackState stateData) 
        : base(enemy, stateMachine, animBoolName, attackPosition, stateData)
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
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isAnimationFinished)
            return;
        
        if (isPlayerMaxAgroRange)
        {
            stateMachine.ChangeState(enemy.playerDetectedState);
        }
        else
        {
            enemy.idleState.SetFlipAfterIdle(false);
            stateMachine.ChangeState(enemy.idleState);
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

    public override void TriggerAttack()
    {
        Collider2D playerObject = Physics2D.OverlapCircle(attackPosition.position, attackDetails[meleeAttackType].attackRadius, entity.entityData.player);

        if (playerObject != null)
        {
            attackDetails[meleeAttackType].position = entity.transform.position;
            Player.Instance.Damage(attackDetails[meleeAttackType], entity, true);

            if (attackDetails[meleeAttackType].knockbackStrength > 0.01f)
            {
                Player.Instance.Knockback(attackDetails[meleeAttackType]);
            }
        }
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }
}
