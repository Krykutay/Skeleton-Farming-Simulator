using System.Collections;
using UnityEngine;

public class SpawnEnemy5 : MonoBehaviour
{
    [SerializeField] float _enemySpawnDuration = 3f;

    void OnEnable()
    {
        E5_DeadState.Died += Enemy5_Died;
    }

    void OnDisable()
    {
        E5_DeadState.Died -= Enemy5_Died;
    }

    void Enemy5_Died(Enemy5 enemy)
    {
        StartCoroutine(SpawnTime(_enemySpawnDuration, enemy));
    }

    IEnumerator SpawnTime(float duration, Enemy5 enemy)
    {
        yield return new WaitForSeconds(duration);
        Enemy5Pool.Instance.Get(enemy.initialPosition, enemy.initialRotation);
    }
}
