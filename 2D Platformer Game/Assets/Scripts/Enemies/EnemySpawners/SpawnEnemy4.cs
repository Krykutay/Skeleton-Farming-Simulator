using System.Collections;
using UnityEngine;

public class SpawnEnemy4 : MonoBehaviour
{
    [SerializeField] float _enemySpawnDuration = 3f;

    void OnEnable()
    {
        E4_DeadState.Died += Enemy4_Died;
    }

    void OnDisable()
    {
        E4_DeadState.Died -= Enemy4_Died;
    }

    void Enemy4_Died(Enemy4 enemy)
    {
        StartCoroutine(SpawnTime(_enemySpawnDuration, enemy));
    }

    IEnumerator SpawnTime(float duration, Enemy4 enemy)
    {
        yield return new WaitForSeconds(duration);
        Enemy4Pool.Instance.Get(enemy.initialPosition, enemy.initialRotation);
    }
}
