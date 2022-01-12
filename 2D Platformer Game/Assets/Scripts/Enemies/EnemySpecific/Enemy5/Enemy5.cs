using System.Collections;
using UnityEngine;

public class Enemy5 : Entity
{
    public E5_IdleState idleState { get; private set; }
    public E5_MoveState moveState { get; private set; }
    public E5_PlayerDetectedState playerDetectedState { get; private set; }
    public E5_ChargeState chargeState { get; private set; }
    public E5_MeleeAttackState meleeAttackState { get; private set; }
    public E5_StunState stunState { get; private set; }
    public E5_DeadState deadState { get; private set; }
    public E5_RespawnState respawnState { get; private set; }

    [SerializeField] D_IdleState _idleStateData;
    [SerializeField] D_MoveState _moveStateData;
    [SerializeField] D_PlayerDetectedState _playerDetectedStateData;
    [SerializeField] D_ChargeState _chargeStateData;
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

        moveState = new E5_MoveState(this, stateMachine, "move", _moveStateData);
        idleState = new E5_IdleState(this, stateMachine, "idle", _idleStateData);
        playerDetectedState = new E5_PlayerDetectedState(this, stateMachine, "playerDetected", _playerDetectedStateData);
        chargeState = new E5_ChargeState(this, stateMachine, "charge", _chargeStateData);
        meleeAttackState = new E5_MeleeAttackState(this, stateMachine, "meleeAttack", _meleeAttackPosition, _meleeAttackStateData);
        stunState = new E5_StunState(this, stateMachine, "stun", _stunStateData);
        deadState = new E5_DeadState(this, stateMachine, "dead", _deadStateData);
        respawnState = new E5_RespawnState(this, stateMachine, "respawn", _respawnStateData);

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

        Enemy5Pool.Instance.ReturnToPool(this);
    }

    public override void RotateBodyToPlayer()
    {
        Vector3 direction;
        float angle;
        Quaternion lookAtRotation;
        bool shouldLeaveChargeState = false;

        if (_resetBodyParts != null)
            StopCoroutine(_resetBodyParts);

        direction = (playerTransform.position - _head.position).normalized;

        if (direction.x > 0f)
        {
            if (facingDirection == -1)
            {
                SetVelocityX(0f);
                Flip();
                shouldLeaveChargeState = true;
            }

            angle = Vector2.SignedAngle(Vector2.right, direction);
            angle = Mathf.Clamp(angle, -25f, 25f);
            lookAtRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            if (facingDirection == 1)
            {
                SetVelocityX(0f);
                Flip();
                shouldLeaveChargeState = true;
            }

            angle = Vector2.SignedAngle(-Vector2.right, direction);
            angle = Mathf.Clamp(angle, -25f, 25f);
            lookAtRotation = Quaternion.AngleAxis(-angle > 25 ? 25 : -angle, Vector3.forward);
        }

        _head.localRotation = Quaternion.Slerp(_head.localRotation, lookAtRotation, Time.deltaTime * 5f);
        _leftArm.localRotation = Quaternion.Slerp(_leftArm.localRotation, lookAtRotation, Time.deltaTime * 5f);
        _rightArm.localRotation = Quaternion.Slerp(_rightArm.localRotation, lookAtRotation, Time.deltaTime * 5f);

        if (shouldLeaveChargeState)
            chargeState.LeaveChargeState();
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
        {
            //Gizmos.DrawWireSphere(_meleeAttackPosition.position, _meleeAttackStateData.attackDetails[0].attackRadius);
            Gizmos.DrawWireCube(_meleeAttackPosition.position, _meleeAttackStateData.attackDetails[0].size);
        }
            
    }
}

