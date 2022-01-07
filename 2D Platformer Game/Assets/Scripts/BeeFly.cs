using System;
using UnityEngine;

public class BeeFly : MonoBehaviour, IDamageable
{
    public static Action<Vector3, Quaternion> OnBeeDied;

    [SerializeField] float _moveSpeed = 5f;
    [SerializeField] float _turnTime = 3f;

    Vector3 _initialPosition;
    Quaternion _initialRotation;

    float _heightVariance;
    float _initialTurnTime;

    void Awake()
    {
        _initialPosition = transform.position;
        _initialRotation = transform.rotation;
    }

    void Start()
    {
        _heightVariance = UnityEngine.Random.Range(0.4f, 1.2f);
        _initialTurnTime = _turnTime;
    }

    void Update()
    {
        _turnTime -= Time.deltaTime;
        if (_turnTime < 0)
        {
            _turnTime = _initialTurnTime;
            _moveSpeed = -_moveSpeed;
            transform.Rotate(0f, 180f, 0f);
        }

        transform.position += new Vector3(_moveSpeed * Time.deltaTime, Mathf.Sin(Time.time) * Time.deltaTime * _heightVariance);
    }

    public bool Damage(AttackDetails attackDetails)
    {
        DeathBloodParticlePool.Instance.Get(transform.position, Quaternion.identity);
        DeathChunkParticlePool.Instance.Get(transform.position, Quaternion.identity);

        OnBeeDied?.Invoke(_initialPosition, _initialRotation);
        BeePool.Instance.ReturnToPool(this);

        return true;
    }

}
