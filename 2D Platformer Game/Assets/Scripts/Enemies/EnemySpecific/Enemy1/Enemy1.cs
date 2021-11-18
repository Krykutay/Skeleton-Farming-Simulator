using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Entity
{
    public E1_IdleState idleState { get; private set; }
    public E1_MoveState moveState { get; private set; }

    [SerializeField] D_IdleState _idleStateData;
    [SerializeField] D_MoveState _moveStateData;
    
    public override void Awake()
    {
        base.Awake();

        moveState = new E1_MoveState(this, stateMachine, "move", _moveStateData, this);
        idleState = new E1_IdleState(this, stateMachine, "idle", _idleStateData, this);
    }

    public override void Start()
    {
        base.Start();

        stateMachine.Initialize(moveState);
    }

}

