using System.Collections;
using UnityEngine;

public class SpawnEnemy6 : MonoBehaviour
{
    [SerializeField] float _enemySpawnDuration = 3f;

    void OnEnable()
    {
        E6_DeadState.Died += Enemy6_Died;
    }

    void OnDisable()
    {
        E6_DeadState.Died -= Enemy6_Died;
    }

    void Enemy6_Died(Enemy6 enemy)
    {
        StartCoroutine(SpawnTime(_enemySpawnDuration, enemy));
    }

    IEnumerator SpawnTime(float duration, Enemy6 enemy)
    {
        yield return new WaitForSeconds(duration);
        Enemy5Pool.Instance.Get(enemy.initialPosition, enemy.initialRotation);
    }
}
