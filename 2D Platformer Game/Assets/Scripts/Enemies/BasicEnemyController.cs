using System;
using UnityEngine;

public class BasicEnemyController : MonoBehaviour
{
    enum State
    {
        Moving,
        Knockback,
        Dead
    }

    State _currentState;

    public static Action<BasicEnemyController> Died;

    [SerializeField] float _groundCheckDistance;
    [SerializeField] float _wallCheckDistance;
    [SerializeField] float _movementSpeed;
    [SerializeField] float _maxHealth;
    [SerializeField] float _knockbackDuration;

    [SerializeField] Vector2 _knockbackSpeed;

    [SerializeField] Transform _groundCheck;
    [SerializeField] Transform _wallCheck;

    [SerializeField] LayerMask _ground;

    GameObject _alive;
    Rigidbody2D _aliveRb;
    Animator _aliveAnim;

    Vector2 _movement;
    Vector3 _initialPosition;
    Quaternion _initialRotation;

    public Vector3 initialPosition { get { return _initialPosition; } }
    public Quaternion initialRotation { get { return _initialRotation; } }

    int _facingDirection;
    int _damageDirection;

    float _currentHealth;
    float _knockbackStartTime;

    bool _groundDetected;
    bool _wallDetected;

    void Awake()
    {
        _alive = transform.Find("Alive").gameObject;
        _aliveRb = _alive.GetComponent<Rigidbody2D>();
        _aliveAnim = _alive.GetComponent<Animator>();

        _initialPosition = transform.position;
        _initialRotation = transform.rotation;
    }

    void OnEnable()
    {
        _alive.transform.localPosition = Vector3.zero;
        _alive.transform.rotation = Quaternion.Euler(Vector3.zero);
        _currentState = State.Moving;
        _facingDirection = 1;
        _currentHealth = _maxHealth;
    }

    void Update()
    {
        switch (_currentState)
        {
            case State.Moving:
                UpdateMovingState();
                break;
            case State.Knockback:
                UpdateKnockbackState();
                break;
            case State.Dead:
                UpdateDeadState();
                break;
        }
    }

    // MOVING STATE -------------------------------------

    void EnterMovingState()
    {

    }

    void UpdateMovingState()
    {
        _groundDetected = Physics2D.Raycast(_groundCheck.position, Vector2.down, _groundCheckDistance, _ground);
        _wallDetected = Physics2D.Raycast(_wallCheck.position, Vector2.right, _wallCheckDistance, _ground);

        if (!_groundDetected || _wallDetected)
        {
            Flip();
        }
        else
        {
            _movement.Set(_movementSpeed * _facingDirection, _aliveRb.velocity.y);
            _aliveRb.velocity = _movement;
        }
    }

    void ExitMovingState()
    {

    }

    // KNOCKBACK STATE ---------------------------------
    void EnterKnockbackState()
    {
        _knockbackStartTime = Time.time;
        _movement.Set(_knockbackSpeed.x * _damageDirection, _knockbackSpeed.y);
        _aliveRb.velocity = _movement;
        _aliveAnim.SetBool("Knockback", true);
    }

    void UpdateKnockbackState()
    {
        if (Time.time >= _knockbackStartTime + _knockbackDuration)
        {
            SwitchState(State.Moving);
        }
    }

    void ExitKnockbackState()
    {
        _aliveAnim.SetBool("Knockback", false);
    }
    
    // DEAD STATE ---------------------------------------
    void EnterDeadState()
    {
        DeathChunkParticlePool.Instance.Get(_alive.transform.position, Quaternion.Euler(0f, 0f, 0f));
        DeathBloodParticlePool.Instance.Get(_alive.transform.position, Quaternion.Euler(0f, 0f, 0f));

        Died?.Invoke(this);
        BasicEnemyPool.Instance.ReturnToPool(this);
    }

    void UpdateDeadState()
    {

    }

    void ExitDeadState()
    {

    }

    // OTHER FUCTIONS ----------------------------------

    void SwitchState(State state)
    {
        switch (_currentState)
        {
            case State.Moving:
                ExitMovingState();
                break;
            case State.Knockback:
                ExitKnockbackState();
                break;
            case State.Dead:
                ExitDeadState();
                break;
        }

        switch (state)
        {
            case State.Moving:
                EnterMovingState();
                break;
            case State.Knockback:
                EnterKnockbackState();
                break;
            case State.Dead:
                EnterDeadState();
                break;
        }

        _currentState = state;
    }

    void Flip()
    {
        _facingDirection *= -1;
        _alive.transform.Rotate(0f, 180f, 0f);
    }

    void Damage(float[] attackDetails)
    {
        _currentHealth -= attackDetails[0];

        Enemy1HitParticlePool.Instance.Get(_alive.transform.position, Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(0, 360)));

        if (attackDetails[1] > _alive.transform.position.x)
        {
            _damageDirection = -1;
        }
        else
        {
            _damageDirection = 1;
        }

        // Hit particle

        if (_currentHealth > 0f)
        {
            SwitchState(State.Knockback);
        }
        else if (_currentHealth <= 0f)
        {
            SwitchState(State.Dead);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(_groundCheck.position, new Vector2(_groundCheck.position.x, _groundCheck.position.y - _groundCheckDistance));

        Gizmos.DrawLine(_wallCheck.position, new Vector2(_wallCheck.position.x + _wallCheckDistance, _wallCheck.position.y));
    }

}
