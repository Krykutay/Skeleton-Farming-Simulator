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

    public Vector3 initialPosition { get; private set; }
    public Quaternion initialRotation { get; private set; }

    [SerializeField] D_IdleState _idleStateData;
    [SerializeField] D_MoveState _moveStateData;
    [SerializeField] D_PlayerDetectedState _playerDetectedStateData;
    [SerializeField] D_ChargeState _chargeStateData;
    [SerializeField] D_LookForPlayerState _lookForPlayerStateData;
    [SerializeField] D_MeleeAttackState _meleeAttackStateData;
    [SerializeField] D_StunState _stunStateData;
    [SerializeField] D_DeadState _deadStateData;

    [SerializeField] Transform _meleeAttackPosition;

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

        if (stateMachine.currentState == deadState)
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

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(_meleeAttackPosition.position, _meleeAttackStateData.attackDetails[0].attackRadius);
    }
}

