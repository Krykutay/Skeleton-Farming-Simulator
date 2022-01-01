using UnityEngine;

public class BirdFly : MonoBehaviour, IDamageable
{
    float _heightVariance;
    [SerializeField] float _moveSpeed = 5f;
    [SerializeField] float _turnTime = 3f;

    float _initialTurnTime;

    void Start()
    {
        _heightVariance = Random.Range(0.6f, 1.4f);
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

        Destroy(gameObject);

        return true;
    }

}
