using System;
using UnityEngine;

public class HealthPowerupTrainScene : MonoBehaviour
{
    SpawnPowerupTrainScene _spawnPowerupTrainScene;

    Vector3 _initialPosition;

    void Awake()
    {
        _initialPosition = transform.position;
    }

    void Start()
    {
        _spawnPowerupTrainScene = PowerupManager.Instance.transform.parent.Find("Gamemanager").GetComponent<SpawnPowerupTrainScene>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        SoundManager.Instance.Play(SoundManager.SoundTags.Powerup);
        PowerupManager.Instance.HealthPowerupCollected();
        HealthPowerupTrainScenePool.Instance.ReturnToPool(this);
        _spawnPowerupTrainScene.SpawnHealthPowerup(_initialPosition);
    }
}
