using System.Linq;
using UnityEngine;

public class SpawnPowerupInMines : MonoBehaviour
{
    [SerializeField] StartSpawns _startSpawns;
    [SerializeField] Transform[] _spawnPositions;

    [SerializeField] float _spawnDuration = 5f;

    float _lastSpawnTime = Mathf.NegativeInfinity;

    bool _isSpawnTriggered;

    int _randomPosition;
    int _randomPowerup;
    int _lastPosition = -1;

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
            var list = Enumerable.Range(0, _spawnPositions.Length).Where(t => t != _lastPosition).ToArray();

            _randomPowerup = Random.Range(0, 100);
            _randomPosition = list[Random.Range(0, list.Length)];

            if (_randomPowerup < 15)
                VaporizePowerupPool.Instance.Get(_spawnPositions[_randomPosition].position, Quaternion.identity);
            else if (_randomPowerup < 50)
                HealthPowerupPool.Instance.Get(_spawnPositions[_randomPosition].position, Quaternion.identity);
            else if (_randomPowerup < 75)
                DamagePowerupPool.Instance.Get(_spawnPositions[_randomPosition].position, Quaternion.identity);
            else
                ShieldPowerupPool.Instance.Get(_spawnPositions[_randomPosition].position, Quaternion.identity);

            _lastSpawnTime = Time.time;
            _lastPosition = _randomPosition;
        }
    }

    void StartSpawns_SpawnsTriggered()
    {
        _isSpawnTriggered = true;
    }
}
