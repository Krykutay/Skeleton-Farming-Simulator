using System.Collections;
using UnityEngine;

public class Enemy3 : Entity
{
    public E3_IdleState idleState { get; private set; }
    public E3_MoveState moveState { get; private set; }
    public E3_PlayerDetectedState playerDetectedState { get; private set; }
    public E3_ChargeState chargeState { get; private set; }
    public E3_LookForPlayerState lookForPlayerState { get; private set; }
    public E3_MeleeAttackState meleeAttackState { get; private set; }
    public E3_StunState stunState { get; private set; }
    public E3_DeadState deadState { get; private set; }
    public E3_RespawnState respawnState { get; private set; }

    [SerializeField] D_IdleState _idleStateData;
    [SerializeField] D_MoveState _moveStateData;
    [SerializeField] D_PlayerDetectedState _playerDetectedStateData;
    [SerializeField] D_ChargeState _chargeStateData;
    [SerializeField] D_LookForPlayerState _lookForPlayerStateData;
    [SerializeField] D_MeleeAttackState _meleeAttackStateData;
    [SerializeField] D_StunState _stunStateData;
    [SerializeField] D_DeadState _deadStateData;
    [SerializeField] D_RespawnState _respawnStateData;

    [SerializeField] Transform _meleeAttackPosition;

    Transform _head;
    Transform _leftArm;
    Transform _rightArm;

    IEnumerator _resetBodyParts;

    public override void Awake()
    {
        base.Awake();

        moveState = new E3_MoveState(this, stateMachine, "move", _moveStateData, this);
        idleState = new E3_IdleState(this, stateMachine, "idle", _idleStateData, this);
        playerDetectedState = new E3_PlayerDetectedState(this, stateMachine, "playerDetected", _playerDetectedStateData, this);
        chargeState = new E3_ChargeState(this, stateMachine, "charge", _chargeStateData, this);
        lookForPlayerState = new E3_LookForPlayerState(this, stateMachine, "lookForPlayer", _lookForPlayerStateData, this);
        meleeAttackState = new E3_MeleeAttackState(this, stateMachine, "meleeAttack", _meleeAttackPosition, _meleeAttackStateData, this);
        stunState = new E3_StunState(this, stateMachine, "stun", _stunStateData, this);
        deadState = new E3_DeadState(this, stateMachine, "dead", _deadStateData, this);
        respawnState = new E3_RespawnState(this, stateMachine, "respawn", _respawnStateData, this);

        _head = transform.Find("Body").Find("MoveHead");
        _leftArm = transform.Find("Body").Find("MoveWeaponArm");
        _rightArm = transform.Find("Body").Find("MoveRightArm");
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
            return true;

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

        Enemy3Pool.Instance.ReturnToPool(this);
    }

    public override void RotateBodyToPlayer()
    {
        Vector3 direction;
        float angle;
        Quaternion lookAtRotation;

        if (_resetBodyParts != null)
            StopCoroutine(_resetBodyParts);

        direction = (playerTransform.position - _head.position).normalized;

        if (direction.x > 0f)
        {
            if (facingDirection == -1)
                Flip();

            angle = Vector2.SignedAngle(Vector2.right, direction);
            angle = Mathf.Clamp(angle, -30f, 30f);
            lookAtRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            if (facingDirection == 1)
                Flip();

            angle = Vector2.SignedAngle(-Vector2.right, direction);
            angle = Mathf.Clamp(angle, -30f, 30f);
            lookAtRotation = Quaternion.AngleAxis(-angle > 30 ? 30 : -angle, Vector3.forward);
        }

        _head.localRotation = Quaternion.Slerp(_head.localRotation, lookAtRotation, Time.deltaTime * 5f);
        _leftArm.localRotation = Quaternion.Slerp(_leftArm.localRotation, lookAtRotation, Time.deltaTime * 5f);
        _rightArm.localRotation = Quaternion.Slerp(_rightArm.localRotation, lookAtRotation, Time.deltaTime * 5f);
    }

    public override void ResetBodyPosition()
    {
        if (_resetBodyParts != null)
            StopCoroutine(_resetBodyParts);

        _resetBodyParts = ResetBodyParts();
        StartCoroutine(_resetBodyParts);
    }

    IEnumerator ResetBodyParts()
    {
        while (Mathf.Abs(_head.localRotation.z) > 0.01f)
        {
            _head.localRotation = Quaternion.Slerp(_head.localRotation, Quaternion.Euler(0f, 0f, 0f), Time.deltaTime * 5f);
            _leftArm.localRotation = Quaternion.Slerp(_leftArm.localRotation, Quaternion.Euler(0f, 0f, 0f), Time.deltaTime * 5f);
            _rightArm.localRotation = Quaternion.Slerp(_rightArm.localRotation, Quaternion.Euler(0f, 0f, 0f), Time.deltaTime * 5f);

            yield return new WaitForFixedUpdate();
        }
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        if (_meleeAttackStateData.attackDetails.Length > 0)
            Gizmos.DrawWireSphere(_meleeAttackPosition.position, _meleeAttackStateData.attackDetails[0].attackRadius);
    }
}

