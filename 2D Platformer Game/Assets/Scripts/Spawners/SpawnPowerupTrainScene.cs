using System.Collections;
using UnityEngine;

public class SpawnPowerupTrainScene : MonoBehaviour
{
    
    [SerializeField] float _powerupSpawnDuration = 8f;

    public void SpawnHealthPowerup(Vector3 initialPosition)
    {
        StartCoroutine(DelaySpawnHealthPowerup(initialPosition));
    }

    public void SpawnDamagePowerup(Vector3 initialPosition)
    {
        StartCoroutine(DelaySpawnDamagePowerup(initialPosition));
    }

    public void SpawnShieldPowerup(Vector3 initialPosition)
    {
        StartCoroutine(DelaySpawnShieldPowerup(initialPosition));
    }

    public void SpawnVaporizePowerup(Vector3 initialPosition)
    {
        StartCoroutine(DelaySpawnVaporizePowerup(initialPosition));
    }

    IEnumerator DelaySpawnHealthPowerup(Vector3 initialPosition)
    {
        yield return new WaitForSeconds(_powerupSpawnDuration);

        HealthPowerupTrainScenePool.Instance.Get(initialPosition, Quaternion.identity);
    }

    IEnumerator DelaySpawnDamagePowerup(Vector3 initialPosition)
    {
        yield return new WaitForSeconds(_powerupSpawnDuration);

        DamagePowerupTrainScenePool.Instance.Get(initialPosition, Quaternion.identity);
    }

    IEnumerator DelaySpawnShieldPowerup(Vector3 initialPosition)
    {
        yield return new WaitForSeconds(_powerupSpawnDuration);

        ShieldPowerupTrainScenePool.Instance.Get(initialPosition, Quaternion.identity);
    }

    IEnumerator DelaySpawnVaporizePowerup(Vector3 initialPosition)
    {
        yield return new WaitForSeconds(_powerupSpawnDuration);

        VaporizePowerupTrainScenePool.Instance.Get(initialPosition, Quaternion.identity);
    }
}
