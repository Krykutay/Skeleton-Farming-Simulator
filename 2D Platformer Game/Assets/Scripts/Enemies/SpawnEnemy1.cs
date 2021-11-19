using System.Collections;
using UnityEngine;

public class SpawnEnemy1 : MonoBehaviour
{
    [SerializeField] float _basicEnemySpawnDuration = 3f;

    void OnEnable()
    {
        E1_DeadState.Died += Enemy1_Died;
    }

    void OnDisable()
    {
        E1_DeadState.Died -= Enemy1_Died;
    }

    void Enemy1_Died(Enemy1 enemy)
    {
        StartCoroutine(SpawnTime(_basicEnemySpawnDuration, enemy));
    }
    
    IEnumerator SpawnTime(float duration, Enemy1 enemy)
    {
        yield return new WaitForSeconds(duration);
        Enemy1Pool.Instance.Get(enemy.initialPosition, enemy.initialRotation);
    }
}
