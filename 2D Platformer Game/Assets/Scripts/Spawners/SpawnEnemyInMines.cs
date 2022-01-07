using UnityEngine;

public class SpawnEnemyInMines : MonoBehaviour
{
    [SerializeField] StartSpawns _startSpawns;
    [SerializeField] Transform[] _spawnPositions;

    [SerializeField] float _spawnDuration = 7.5f;

    float _lastSpawnTime = Mathf.NegativeInfinity;

    bool _isSpawnTriggered;

    int _randomPosition;
    int _randomEnemy;

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

            _lastSpawnTime = Time.time;
            if (_spawnDuration > 2.5f)
                _spawnDuration -= 0.1f;
        }
    }

    void StartSpawns_SpawnsTriggered()
    {
        _isSpawnTriggered = true;
    }
}
