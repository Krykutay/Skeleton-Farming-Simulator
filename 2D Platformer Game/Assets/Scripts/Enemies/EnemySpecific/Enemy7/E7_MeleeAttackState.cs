using UnityEngine;

public class E7_MeleeAttackState : MeleeAttackState
{
    Enemy7 enemy;

    public E7_MeleeAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_MeleeAttackState stateData, Enemy7 enemy) 
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
            Player.Instance.Damage(attackDetails[meleeAttackType]);

            if (attackDetails[meleeAttackType].knockbackStrength > 0.01f)
            {
                attackDetails[meleeAttackType].position = enemy.transform.position;
                Player.Instance.Knockback(attackDetails[meleeAttackType]);
            }
        }
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }
}
