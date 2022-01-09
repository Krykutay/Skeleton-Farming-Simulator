using UnityEngine;

public class SpawnEnemyInMines : MonoBehaviour
{
    [SerializeField] StartSpawns _startSpawns;
    [SerializeField] Transform[] _spawnPositions;

    [SerializeField] float _spawnDuration = 10f;
    [SerializeField] int _maxEnemyCount = 25;

    float _lastSpawnTime = Mathf.NegativeInfinity;

    bool _isSpawnTriggered;

    int _randomPosition;
    int _randomEnemy;
    int _activeEnemyCount;

    void OnEnable()
    {
        _activeEnemyCount = 5;
        _startSpawns.SpawnsTriggered += StartSpawns_SpawnsTriggered;
        Entity.OnEnemyDied += Entity_EnemyDied;
    }

    void OnDisable()
    {
        _startSpawns.SpawnsTriggered -= StartSpawns_SpawnsTriggered;
        Entity.OnEnemyDied -= Entity_EnemyDied;
    }

    void Update()
    {
        if (!_isSpawnTriggered)
            return;

        if (Time.time > _lastSpawnTime + _spawnDuration && _activeEnemyCount <= _maxEnemyCount)
        {
            _randomPosition = Random.Range(0, _spawnPositions.Length);
            _randomEnemy = Random.Range(0, 6);

            switch (_randomEnemy)
            {
                default:
                case 0:
                    Enemy3Pool.Instance.Get(_spawnPositions[_randomPosition].position, Quaternion.identity);
                    break;
                case 1:
                    Enemy4Pool.Instance.Get(_spawnPositions[_randomPosition].position, Quaternion.identity);
                    break;
                case 2:
                    Enemy5Pool.Instance.Get(_spawnPositions[_randomPosition].position, Quaternion.identity);
                    break;
                case 3:
                    Enemy6Pool.Instance.Get(_spawnPositions[_randomPosition].position, Quaternion.identity);
                    break;
                case 4:
                    Enemy7Pool.Instance.Get(_spawnPositions[_randomPosition].position, Quaternion.identity);
                    break;
            }

            _activeEnemyCount++;
            _lastSpawnTime = Time.time;
            if (_spawnDuration > 5f)
                _spawnDuration -= 0.1f;
        }
    }

    void StartSpawns_SpawnsTriggered()
    {
        _isSpawnTriggered = true;
    }

    void Entity_EnemyDied(Entity entity)
    {
        _activeEnemyCount--;
    }
}
