using System.Collections;
using UnityEngine;

public class SpawnBasicEnemy : MonoBehaviour
{
    [SerializeField] float _basicEnemySpawnDuration = 3f;

    void OnEnable()
    {
        BasicEnemyController.Died += BasicEnemy_Died;
    }

    void OnDisable()
    {
        BasicEnemyController.Died -= BasicEnemy_Died;
    }

    void BasicEnemy_Died(BasicEnemyController basicEnemy)
    {
        StartCoroutine(SpawnTime(_basicEnemySpawnDuration, basicEnemy));
    }
    
    IEnumerator SpawnTime(float duration, BasicEnemyController basicEnemy)
    {
        yield return new WaitForSeconds(duration);
        BasicEnemyPool.Instance.ReturnToPool(basicEnemy);
        BasicEnemyPool.Instance.Get(basicEnemy.initialPosition, basicEnemy.initialRotation);
    }
}
