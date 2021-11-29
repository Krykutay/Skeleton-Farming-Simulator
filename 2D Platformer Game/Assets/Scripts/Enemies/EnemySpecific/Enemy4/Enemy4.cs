using System.Collections;
using UnityEngine;

public class Enemy4 : Entity
{
    public E4_MoveState moveState { get; private set; }
    public E4_IdleState idleState { get; private set; }
    public E4_PlayerDetectedState playerDetectedState { get; private set; }
    public E4_LookForPlayerState lookForPlayerState { get; private set; }
    public E4_MeleeAttackState meleeAttackState { get; private set; }
    public E4_StunState stunState { get; private set; }
    public E4_DeadState deadState { get; private set; }
    public E4_DodgeState dodgeState { get; private set; }
    public E4_RangeAttackState rangeAttackState { get; private set; }
    public E4_RespawnState respawnState { get; private set; }

    public Vector3 initialPosition { get; private set; }
    public Quaternion initialRotation { get; private set; }

    [SerializeField] D_MoveState _moveStateData;
    [SerializeField] D_IdleState _idleStateData;
    [SerializeField] D_PlayerDetectedState _playerDetectedStateData;
    [SerializeField] D_LookForPlayerState _lookForPlayerStateData;
    [SerializeField] D_MeleeAttackState _meleeAttackStateData;
    [SerializeField] D_StunState _stunStateData;
    [SerializeField] D_DeadState _deadStateData;
    [SerializeField] D_DodgeState _dodgeStateData;
    [SerializeField] D_RangeAttackState _rangeAttackStateData;
    [SerializeField] D_RespawnState _respawnStateData;

    [SerializeField] Transform _ledgeBehindCheck;
    [SerializeField] Transform _minDodgeDistanceCheck;
    [SerializeField] Transform _meleeAttackPosition;
    [SerializeField] Transform _rangeAttackPosition;

    public D_DodgeState dodgeStateData  { get { return _dodgeStateData; } }

    Transform _head;
    Transform _leftArm;
    Transform _rightArm;

    IEnumerator _resetBodyParts;

    public override void Awake()
    {
        base.Awake();

        moveState = new E4_MoveState(this, stateMachine, "move", _moveStateData, this);
        idleState = new E4_IdleState(this, stateMachine, "idle", _idleStateData, this);
        playerDetectedState = new E4_PlayerDetectedState(this, stateMachine, "playerDetected", _playerDetectedStateData, this);
        lookForPlayerState = new E4_LookForPlayerState(this, stateMachine, "lookForPlayer", _lookForPlayerStateData, this);
        meleeAttackState = new E4_MeleeAttackState(this, stateMachine, "meleeAttack", _meleeAttackPosition, _meleeAttackStateData, this);
        stunState = new E4_StunState(this, stateMachine, "stun", _stunStateData, this);
        deadState = new E4_DeadState(this, stateMachine, "dead", _deadStateData, this);
        dodgeState = new E4_DodgeState(this, stateMachine, "dodge", _dodgeStateData, this);
        rangeAttackState = new E4_RangeAttackState(this, stateMachine, "rangeAttack", _rangeAttackPosition, _rangeAttackStateData, this);
        respawnState = new E4_RespawnState(this, stateMachine, "respawn", _respawnStateData, this);

        initialPosition = transform.position;
        initialRotation = transform.rotation;

        _head = transform.Find("Body").Find("MoveHead");
        _leftArm = transform.Find("Body").Find("MoveWeaponArm");
        _rightArm = transform.Find("Body").Find("MoveRightArm");
    }

    public override void OnEnable()
    {
        base.OnEnable();

        stateMachine.Initialize(moveState);
    }

    public override bool Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);

        if (stateMachine.currentState == deadState || stateMachine.currentState == respawnState)
            return true;

        Enemy1HitParticlePool.Instance.Get(transform.position, Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(0, 360)));

        if (isDead)
        {
            stateMachine.ChangeState(deadState);
        }
        else if (isStunned && stateMachine.currentState != stunState)
        {
            stateMachine.ChangeState(stunState);
        }

        return true;
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

            angle = Mathf.Clamp(angle, -40f, 40f);
             _headLookAtRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            if (facingDirection == 1)
                Flip();

            angle = Vector2.SignedAngle(-Vector2.right, direction);
            _bodyLookAtRotation = Quaternion.AngleAxis(-angle, Vector3.forward);

            angle = Mathf.Clamp(angle, -40f, 40f);
            _headLookAtRotation = Quaternion.AngleAxis(-angle > 40 ? 40 : -angle, Vector3.forward);
        }

        _head.localRotation = Quaternion.Slerp(_head.localRotation, _headLookAtRotation, Time.deltaTime * 5f);
        _leftArm.localRotation = Quaternion.Slerp(_leftArm.localRotation, _bodyLookAtRotation, Time.deltaTime * 5f);
        _rightArm.localRotation = Quaternion.Slerp(_rightArm.localRotation, _bodyLookAtRotation, Time.deltaTime * 5f);
        _rangeAttackPosition.localRotation = Quaternion.Slerp(_rangeAttackPosition.localRotation, _bodyLookAtRotation, Time.deltaTime * 5f);
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
            _rangeAttackPosition.localRotation = Quaternion.Slerp(_rangeAttackPosition.localRotation, Quaternion.Euler(0f, 0f, 0f), Time.deltaTime * 5f);

            yield return new WaitForFixedUpdate();
        }
    }

    public override bool CheckLedgeBehind()
    {
        return Physics2D.Raycast(_ledgeBehindCheck.position, Vector2.down, entityData.ledgeBehindCheckDistance, entityData.ground);
    }

    public override bool CheckMinDodgeDistance()
    {
        return Physics2D.Raycast(_minDodgeDistanceCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.ground);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(_meleeAttackPosition.position, _meleeAttackStateData.attackDetails[0].attackRadius);
    }

}
