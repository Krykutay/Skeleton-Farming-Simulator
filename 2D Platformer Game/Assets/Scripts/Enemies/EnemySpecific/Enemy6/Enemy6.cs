using System.Collections;
using UnityEngine;

public class Enemy6 : Entity
{
    public E6_MoveState moveState { get; private set; }
    public E6_IdleState idleState { get; private set; }
    public E6_PlayerDetectedState playerDetectedState { get; private set; }
    public E6_MeleeAttackState meleeAttackState { get; private set; }
    public E6_StunState stunState { get; private set; }
    public E6_DeadState deadState { get; private set; }
    public E6_TeleportState teleportState { get; private set; }
    public E6_RangeAttackState rangeAttackState { get; private set; }
    public E6_RespawnState respawnState { get; private set; }

    [SerializeField] D_MoveState _moveStateData;
    [SerializeField] D_IdleState _idleStateData;
    [SerializeField] D_PlayerDetectedState _playerDetectedStateData;
    [SerializeField] D_MeleeAttackState _meleeAttackStateData;
    [SerializeField] D_StunState _stunStateData;
    [SerializeField] D_DeadState _deadStateData;
    [SerializeField] D_TeleportState _teleportStateData;
    [SerializeField] D_RangeAttackState _rangeAttackStateData;
    [SerializeField] D_RespawnState _respawnStateData;

    [SerializeField] Transform _ledgeBehindCheck;
    [SerializeField] Transform _meleeAttackPosition;
    [SerializeField] Transform _rangeAttackPosition;

    public D_TeleportState teleportStateData  { get { return _teleportStateData; } }

    Transform _head;

    IEnumerator _resetBodyParts;

    public override void Awake()
    {
        base.Awake();

        moveState = new E6_MoveState(this, stateMachine, "move", _moveStateData);
        idleState = new E6_IdleState(this, stateMachine, "idle", _idleStateData);
        playerDetectedState = new E6_PlayerDetectedState(this, stateMachine, "playerDetected", _playerDetectedStateData);
        meleeAttackState = new E6_MeleeAttackState(this, stateMachine, "meleeAttack", _meleeAttackPosition, _meleeAttackStateData);
        stunState = new E6_StunState(this, stateMachine, "stun", _stunStateData);
        deadState = new E6_DeadState(this, stateMachine, "dead", _deadStateData);
        teleportState = new E6_TeleportState(this, stateMachine, "teleport", _teleportStateData);
        rangeAttackState = new E6_RangeAttackState(this, stateMachine, "rangeAttack", _rangeAttackPosition, _rangeAttackStateData);
        respawnState = new E6_RespawnState(this, stateMachine, "respawn", _respawnStateData);

        _head = transform.Find("Body").Find("MoveHead");
    }

    public override void OnEnable()
    {
        base.OnEnable();

        ResetBodyPosition();
        stateMachine.Initialize(moveState);
    }

    public override bool Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);

        if (stateMachine.currentState == deadState || stateMachine.currentState == respawnState)
            return false;

        if (isDead)
        {
            JustDied();
        }
        else if (isStunned && stateMachine.currentState != stunState)
        {
            stateMachine.ChangeState(stunState);
        }

        return true;
    }

    public override void DamageBySurface()
    {
        base.DamageBySurface();

        if (stateMachine.currentState == deadState || stateMachine.currentState == respawnState)
            return;

        if (isDead)
            JustDied();
    }

    public override void JustDied()
    {
        base.JustDied();

        stateMachine.ChangeState(deadState);
    }

    public override void StunnedByPlayerParry(int parryDirection)
    {
        base.StunnedByPlayerParry(parryDirection);

        if (stateMachine.currentState != deadState || stateMachine.currentState != respawnState)
            stateMachine.ChangeState(stunState);
    }

    public override void PowerupManager_Vaporize()
    {
        base.PowerupManager_Vaporize();

        Enemy6Pool.Instance.ReturnToPool(this);
    }

    public override void DamageHop(float velocity)
    {
        if (stateMachine.currentState != teleportState)
            rb.velocity = new Vector2(rb.velocity.x, velocity);
    }

    public override void RotateBodyToPlayer()
    {
        Vector3 direction;
        float angle;
        Quaternion _bodyLookAtRotation;
        Quaternion _headLookAtRotation;

        if (_resetBodyParts != null)
            StopCoroutine(_resetBodyParts);

        direction = (playerTransform.position - _head.position).normalized;

        if (direction.x > 0f)
        {
            if (facingDirection == -1)
                Flip();

            angle = Vector2.SignedAngle(Vector2.right, direction);
            _bodyLookAtRotation = Quaternion.AngleAxis(angle, Vector3.forward);

            angle = Mathf.Clamp(angle, -20f, 20f);
             _headLookAtRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            if (facingDirection == 1)
                Flip();

            angle = Vector2.SignedAngle(-Vector2.right, direction);
            _bodyLookAtRotation = Quaternion.AngleAxis(-angle, Vector3.forward);

            angle = Mathf.Clamp(angle, -20f, 20f);
            _headLookAtRotation = Quaternion.AngleAxis(-angle > 40 ? 40 : -angle, Vector3.forward);
        }

        _head.localRotation = Quaternion.Slerp(_head.localRotation, _headLookAtRotation, Time.deltaTime * 5f);
        _rangeAttackPosition.localRotation = _bodyLookAtRotation;
    }

    public override void ResetBodyPosition()
    {
        if (_resetBodyParts != null)
            StopCoroutine(_resetBodyParts);

        _rangeAttackPosition.localRotation = Quaternion.Euler(0f, 0f, 0f);

        _resetBodyParts = ResetBodyParts();
        StartCoroutine(_resetBodyParts);
    }

    IEnumerator ResetBodyParts()
    {
        while (Mathf.Abs(_head.localRotation.z) > 0.01f)
        {
            _head.localRotation = Quaternion.Slerp(_head.localRotation, Quaternion.Euler(0f, 0f, 0f), Time.deltaTime * 5f);

            yield return new WaitForFixedUpdate();
        }
    }

    public override bool CheckLedgeBehind()
    {
        return Physics2D.Raycast(_ledgeBehindCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.ground);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        if (_meleeAttackStateData.attackDetails.Length > 0)
            Gizmos.DrawWireCube(_meleeAttackPosition.position, _meleeAttackStateData.attackDetails[0].size);
    }

}
