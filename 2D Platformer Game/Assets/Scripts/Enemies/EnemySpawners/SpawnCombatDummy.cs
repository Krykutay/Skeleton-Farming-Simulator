using System.Collections;
using UnityEngine;

public class SpawnCombatDummy : MonoBehaviour
{
    [SerializeField] float _combatDummySpawnTime = 3f;

    void OnEnable()
    {
        CombatDummyController.Died += CombatDummy_Died;
    }

    void OnDisable()
    {
        CombatDummyController.Died -= CombatDummy_Died;
    }

    void CombatDummy_Died(CombatDummyController combatDummy)
    {
        StartCoroutine(SpawnTime(_combatDummySpawnTime, combatDummy));
    }

    IEnumerator SpawnTime(float duration, CombatDummyController combatDummy)
    {
        yield return new WaitForSeconds(duration);
        CombatDummyPool.Instance.ReturnToPool(combatDummy);
        CombatDummyPool.Instance.Get(combatDummy.initialPosition, combatDummy.initialRotation);
    }
}
