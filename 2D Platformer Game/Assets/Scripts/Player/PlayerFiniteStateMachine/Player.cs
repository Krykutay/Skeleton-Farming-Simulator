using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region State Variable
    [SerializeField] PlayerData playerData;

    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    #endregion

    #region Components
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public PlayerInputHandler inputHandler { get; private set; }
    #endregion

    public Vector2 currentVelocity { get; private set; }
    public int facingDirection { get; private set; }

    Vector2 _workSpace;

    #region Unity Callback Functions
    void Awake()
    {
        anim = GetComponent<Animator>();
        inputHandler = GetComponent<PlayerInputHandler>();
        rb = GetComponent<Rigidbody2D>();

        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, playerData, "idle");
        moveState = new PlayerMoveState(this, stateMachine, playerData, "move");
    }

    void OnEnable()
    {
        stateMachine.Initialize(idleState);
        facingDirection = 1;
    }

    void Update()
    {
        currentVelocity = rb.velocity;
        stateMachine.currentState.LogicUpdate();
    }

    void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }
    #endregion

    #region Set Functions
    public void SetVelocityX(float velocity)
    {
        _workSpace.Set(velocity, currentVelocity.y);
        rb.velocity = _workSpace;
        currentVelocity = _workSpace;
    }
    #endregion

    #region Check Functions
    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != facingDirection)
        {
            Flip();
        }
    }
    #endregion

    void Flip()
    {
        facingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
    }
}
