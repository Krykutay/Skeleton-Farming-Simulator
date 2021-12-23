using System;
using UnityEngine;

public class HealthPowerup : MonoBehaviour
{
    [SerializeField] float _duration;

    float _spawnTime;

    void OnEnable()
    {
        _spawnTime = Time.time;
    }

    void Update()
    {
        if (Time.time >= _spawnTime + _duration)
        {
            HealthPowerupPool.Instance.ReturnToPool(this);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        SoundManager.Instance.Play(SoundManager.SoundTags.Powerup);
        PowerupManager.Instance.HealthPowerupCollected();
        HealthPowerupPool.Instance.ReturnToPool(this);
    }
}
