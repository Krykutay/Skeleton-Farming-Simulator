using UnityEngine;

public class TeleportState : State
{
    protected D_TeleportState stateData;

    protected bool performedCloseRangeAction;
    protected bool isPlayerInMaxAgroRange;
    protected bool isGrounded;
    protected bool isTeleportOver;

    bool _isLedgeBehind;
    bool _isLedgeForward;
    bool _isLedgeDetectionActionTaken;
    bool _isTeleportStarted;
    bool _isTeleportBackAnimTriggered;

    int _teleportDirection;

    Vector3 _minTeleportForwardLength;
    Vector3 _minTeleportBackLength;

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

        if (!_isTeleportStarted)
            return;

        if ((_teleportDirection == entity.facingDirection && !_isLedgeForward) || 
            (_teleportDirection != entity.facingDirection && !_isLedgeBehind) ||
            Time.time >= startTime + stateData.teleportDuration)
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
        {
            _isLedgeForward = entity.CheckLedge();
            _isLedgeBehind = entity.CheckLedgeBehind();
        }
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

        entity.rb.velocity = new Vector2(stateData.teleportSpeed * _teleportDirection, 0f);
    }

    public void TeleportEnded()
    {
        isTeleportOver = true;
    }

    public bool TransitionToTeleportState()
    {
        _minTeleportForwardLength.Set(entity.transform.position.x + 1.5f * entity.facingDirection, entity.transform.position.y, entity.transform.position.z);
        _minTeleportBackLength.Set(entity.transform.position.x - 1.5f * entity.facingDirection, entity.transform.position.y, entity.transform.position.z);

        bool canTeleportFoward = CheckCanTeleport(_minTeleportForwardLength);
        bool canTeleportBack = CheckCanTeleport(_minTeleportBackLength);

        float randomAction = Random.Range(0, 2);

        if (randomAction == 0)
        {
            if (canTeleportFoward)
            {
                _teleportDirection = entity.facingDirection;
                return true;
            }
            else if (canTeleportBack)
            {
                _teleportDirection = -entity.facingDirection;
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (canTeleportBack)
            {
                _teleportDirection = -entity.facingDirection;
                return true;
            }
            else if (canTeleportFoward)
            {
                _teleportDirection = entity.facingDirection;
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}
