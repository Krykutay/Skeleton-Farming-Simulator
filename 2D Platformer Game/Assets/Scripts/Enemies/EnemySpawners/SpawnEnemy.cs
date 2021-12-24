using System.Collections;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] float _enemySpawnDuration = 3f;

    void OnEnable()
    {
        Entity.Died += Entity_Died;
    }

    void OnDisable()
    {
        Entity.Died -= Entity_Died;
    }

    void Entity_Died(Entity entity)
    {
        StartCoroutine(SpawnTime(_enemySpawnDuration, entity));
    }

    IEnumerator SpawnTime(float duration, Entity entity)
    {
        yield return new WaitForSeconds(duration);

        if (entity is Enemy3)
            Enemy3Pool.Instance.Get(entity.initialPosition, entity.initialRotation);
        else if (entity is Enemy4)
            Enemy4Pool.Instance.Get(entity.initialPosition, entity.initialRotation);
        else if (entity is Enemy5)
            Enemy5Pool.Instance.Get(entity.initialPosition, entity.initialRotation);
        else if (entity is Enemy6)
            Enemy6Pool.Instance.Get(entity.initialPosition, entity.initialRotation);
        else if (entity is Enemy7)
            Enemy7Pool.Instance.Get(entity.initialPosition, entity.initialRotation);
    }
}
