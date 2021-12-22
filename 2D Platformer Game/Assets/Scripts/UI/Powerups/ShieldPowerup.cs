using System;
using UnityEngine;

public class ShieldPowerup : MonoBehaviour
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
            ShieldPowerupPool.Instance.ReturnToPool(this);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // play sound
        PowerupManager.Instance.ShieldPowerupCollected();
        ShieldPowerupPool.Instance.ReturnToPool(this);
    }
}
