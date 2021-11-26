using System.Collections;
using UnityEngine;

public class SpawnEnemy3 : MonoBehaviour
{
    [SerializeField] float _enemySpawnDuration = 3f;

    void OnEnable()
    {
        E3_DeadState.Died += Enemy3_Died;
    }

    void OnDisable()
    {
        E3_DeadState.Died -= Enemy3_Died;
    }

    void Enemy3_Died(Enemy3 enemy)
    {
        StartCoroutine(SpawnTime(_enemySpawnDuration, enemy));
    }

    IEnumerator SpawnTime(float duration, Enemy3 enemy)
    {
        yield return new WaitForSeconds(duration);
        Enemy3Pool.Instance.Get(enemy.initialPosition, enemy.initialRotation);
    }
}
