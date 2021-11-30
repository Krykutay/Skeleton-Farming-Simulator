using UnityEngine;

public class TeleportState : State
{
    protected D_TeleportState stateData;

    protected bool performedCloseRangeAction;
    protected bool isPlayerInMaxAgroRange;
    protected bool isGrounded;
    protected bool isDodgeOver;

    bool _isLedgeBehind;
    bool _isLedgeDetectionActionTaken;

    public TeleportState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_TeleportState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        isDodgeOver = false;
        _isLedgeDetectionActionTaken = false;
        entity.SetVelocity(stateData.dodgeSpeed, stateData.dodgeAngle, -entity.facingDirection);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (!_isLedgeBehind && !_isLedgeDetectionActionTaken)
        {
            entity.SetVelocityX(0f);
            _isLedgeDetectionActionTaken = true;
        }

        if (Time.time >= startTime + stateData.dodgeTime && isGrounded)
        {
            isDodgeOver = true;
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();

        performedCloseRangeAction = entity.CheckPlayerInMeleeRangeAction();
        isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();
        isGrounded = entity.CheckGround();

        if (!_isLedgeDetectionActionTaken)
            _isLedgeBehind = entity.CheckLedgeBehind();
    }
}
