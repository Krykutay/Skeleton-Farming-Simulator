using UnityEngine;

public class TeleportState : State
{
    protected D_TeleportState stateData;

    protected bool performedCloseRangeAction;
    protected bool isPlayerInMaxAgroRange;
    protected bool isGrounded;
    protected bool isTeleportOver;

    bool _isLedgeBehind;
    bool _isLedgeDetectionActionTaken;
    bool _isTeleportStarted;
    bool _isTeleportBackAnimTriggered;

    public TeleportState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_TeleportState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        entity.atsm.teleportState = this;
        _isTeleportStarted = false;
        isTeleportOver = false;
        _isLedgeDetectionActionTaken = false;
        _isTeleportBackAnimTriggered = false;
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

        if (_isTeleportBackAnimTriggered)
            return;

        if (_isTeleportStarted && (!_isLedgeBehind || Time.time >= startTime + stateData.teleportDuration))
        {
            entity.SetVelocityX(0f);
            entity.anim.SetTrigger("teleportBack");
            _isLedgeDetectionActionTaken = true;
            _isTeleportBackAnimTriggered = true;
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

    public bool CheckCanTeleport(Vector3 position)
    {
        bool isLedge = !Physics2D.Raycast(position, Vector2.down, entity.entityData.ledgeCheckDistance, entity.entityData.ground);
        bool isWall = Physics2D.Raycast(position, -Vector2.right, entity.entityData.wallCheckDistance, entity.entityData.ground);

        return (!isLedge && !isWall);
    }

    public void TeleportStarted()
    {
        _isTeleportStarted = true;
        startTime = Time.time;
        entity.SetVelocity(stateData.teleportSpeed, Vector2.right, -entity.facingDirection);
    }

    public void TeleportEnded()
    {
        isTeleportOver = true;
    }
}
