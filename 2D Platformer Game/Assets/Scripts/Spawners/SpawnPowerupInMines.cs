using UnityEngine;

public class SpawnPowerupInMines : MonoBehaviour
{
    [SerializeField] StartSpawns _startSpawns;
    [SerializeField] Transform[] _spawnPositions;

    [SerializeField] float _spawnDuration = 6f;

    float _lastSpawnTime = Mathf.NegativeInfinity;

    bool _isSpawnTriggered;

    int _randomPosition;
    int _randomPowerup;

    void OnEnable()
    {
        _startSpawns.SpawnsTriggered += StartSpawns_SpawnsTriggered;
    }

    void OnDisable()
    {
        _startSpawns.SpawnsTriggered -= StartSpawns_SpawnsTriggered;
    }

    void Update()
    {
        if (!_isSpawnTriggered)
            return;

        if (Time.time > _lastSpawnTime + _spawnDuration)
        {
            _randomPowerup = Random.Range(0, 100);
            _randomPosition = Random.Range(0, _spawnPositions.Length);

            if (_randomPowerup < 15)
                VaporizePowerupPool.Instance.Get(_spawnPositions[_randomPosition].position, Quaternion.identity);
            else if (_randomPowerup < 50)
                HealthPowerupPool.Instance.Get(_spawnPositions[_randomPosition].position, Quaternion.identity);
            else if (_randomPowerup < 75)
                DamagePopupPool.Instance.Get(_spawnPositions[_randomPosition].position, Quaternion.identity);
            else
                ShieldPowerupPool.Instance.Get(_spawnPositions[_randomPosition].position, Quaternion.identity);

            _lastSpawnTime = Time.time;
        }
    }

    void StartSpawns_SpawnsTriggered()
    {
        _isSpawnTriggered = true;
    }
}
