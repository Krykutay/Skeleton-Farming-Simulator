using UnityEngine;

public class E4_DodgeState : DodgeState
{
    Enemy4 enemy;

    public E4_DodgeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DodgeState stateData, Enemy4 enemy)
        : base(entity, stateMachine, animBoolName, stateData)
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

        entity.SetVelocityX(0f);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        enemy.anim.SetFloat("yVelocity", enemy.rb.velocity.y);

        if (!isDodgeOver)
            return;

        if (isPlayerInMaxAgroRange && performedCloseRangeAction)
        {
            stateMachine.ChangeState(enemy.meleeAttackState);
        }
        else if (isPlayerInMaxAgroRange && !performedCloseRangeAction)
        {
            stateMachine.ChangeState(enemy.rangeAttackState);
        }
        else if (!isPlayerInMaxAgroRange)
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

}
