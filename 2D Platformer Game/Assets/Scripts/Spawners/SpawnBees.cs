using System.Collections;
using UnityEngine;

public class SpawnBees : MonoBehaviour
{
    [SerializeField] float _spawnBeeDuration = 8f;

    void OnEnable()
    {
        BeeFly.OnBeeDied += BeeFly_BeeDied;
    }

    void OnDisable()
    {
        BeeFly.OnBeeDied -= BeeFly_BeeDied;
    }

    void BeeFly_BeeDied(Vector3 position, Quaternion rotation)
    {
        StartCoroutine(SpawnBee(position, rotation));
    }

    IEnumerator SpawnBee(Vector3 position, Quaternion rotation)
    {
        yield return new WaitForSeconds(_spawnBeeDuration);

        BeePool.Instance.Get(position, rotation);
    }
}
