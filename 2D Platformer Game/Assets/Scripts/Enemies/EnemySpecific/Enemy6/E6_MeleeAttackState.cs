using UnityEngine;

public class E6_MeleeAttackState : MeleeAttackState
{
    Enemy6 enemy;

    public E6_MeleeAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_MeleeAttackState stateData, Enemy6 enemy)
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
        Collider2D playerObject = Physics2D.OverlapBox(attackPosition.position, attackDetails[meleeAttackType].size, 0f, entity.entityData.player);

        if (playerObject != null)
        {
            Player.Instance.Damage(attackDetails[meleeAttackType]);
        }
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }
}