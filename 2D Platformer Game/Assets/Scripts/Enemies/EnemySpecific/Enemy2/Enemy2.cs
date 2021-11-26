using UnityEngine;

public class Enemy2 : Entity
{
    public E2_MoveState moveState { get; private set; }
    public E2_IdleState idleState { get; private set; }
    public E2_PlayerDetectedState playerDetectedState { get; private set; }
    public E2_LookForPlayerState lookForPlayerState { get; private set; }
    public E2_MeleeAttackState meleeAttackState { get; private set; }
    public E2_StunState stunState { get; private set; }
    public E2_DeadState deadState { get; private set; }
    public E2_DodgeState dodgeState { get; private set; }
    public E2_RangeAttackState rangeAttackState { get; private set; }

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

    [SerializeField] Transform _meleeAttackPosition;
    [SerializeField] Transform _rangeAttackPosition;

    public D_DodgeState dodgeStateData  { get { return _dodgeStateData; } }

    public override void Awake()
    {
        base.Awake();

        moveState = new E2_MoveState(this, stateMachine, "move", _moveStateData, this);
        idleState = new E2_IdleState(this, stateMachine, "idle", _idleStateData, this);
        playerDetectedState = new E2_PlayerDetectedState(this, stateMachine, "playerDetected", _playerDetectedStateData, this);
        lookForPlayerState = new E2_LookForPlayerState(this, stateMachine, "lookForPlayer", _lookForPlayerStateData, this);
        meleeAttackState = new E2_MeleeAttackState(this, stateMachine, "meleeAttack", _meleeAttackPosition, _meleeAttackStateData, this);
        stunState = new E2_StunState(this, stateMachine, "stun", _stunStateData, this);
        deadState = new E2_DeadState(this, stateMachine, "dead", _deadStateData, this);
        dodgeState = new E2_DodgeState(this, stateMachine, "dodge", _dodgeStateData, this);
        rangeAttackState = new E2_RangeAttackState(this, stateMachine, "rangeAttack", _rangeAttackPosition, _rangeAttackStateData, this);

        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    public override void OnEnable()
    {
        base.OnEnable();

        stateMachine.Initialize(moveState);
    }

    public override bool Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);

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

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(_meleeAttackPosition.position, _meleeAttackStateData.attackDetails[0].attackRadius);
    }

}
