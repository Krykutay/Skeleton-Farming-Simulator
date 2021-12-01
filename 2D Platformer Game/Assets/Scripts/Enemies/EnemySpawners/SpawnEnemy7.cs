using System.Collections;
using UnityEngine;

public class SpawnEnemy7 : MonoBehaviour
{
    [SerializeField] float _enemySpawnDuration = 3f;

    void OnEnable()
    {
        E7_DeadState.Died += Enemy7_Died;
    }

    void OnDisable()
    {
        E7_DeadState.Died -= Enemy7_Died;
    }

    void Enemy7_Died(Enemy7 enemy)
    {
        StartCoroutine(SpawnTime(_enemySpawnDuration, enemy));
    }

    IEnumerator SpawnTime(float duration, Enemy7 enemy)
    {
        yield return new WaitForSeconds(duration);
        Enemy7Pool.Instance.Get(enemy.initialPosition, enemy.initialRotation);
    }
}
