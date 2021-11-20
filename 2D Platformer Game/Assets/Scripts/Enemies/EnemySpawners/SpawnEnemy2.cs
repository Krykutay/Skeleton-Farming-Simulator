using System.Collections;
using UnityEngine;

public class SpawnEnemy2 : MonoBehaviour
{
    [SerializeField] float _basicEnemySpawnDuration = 3f;

    void OnEnable()
    {
        E2_DeadState.Died += Enemy2_Died;
    }

    void OnDisable()
    {
        E2_DeadState.Died -= Enemy2_Died;
    }

    void Enemy2_Died(Enemy2 enemy)
    {
        StartCoroutine(SpawnTime(_basicEnemySpawnDuration, enemy));
    }

    IEnumerator SpawnTime(float duration, Enemy2 enemy)
    {
        yield return new WaitForSeconds(duration);
        Enemy2Pool.Instance.Get(enemy.initialPosition, enemy.initialRotation);
    }
}
