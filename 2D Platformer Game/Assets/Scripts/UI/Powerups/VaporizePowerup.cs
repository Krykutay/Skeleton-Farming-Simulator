using System;
using UnityEngine;

public class VaporizePowerup : MonoBehaviour
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
            VaporizePowerupPool.Instance.ReturnToPool(this);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // play sound
        PowerupManager.Instance.VaporizePowerupCollected();
        VaporizePowerupPool.Instance.ReturnToPool(this);
    }
}
