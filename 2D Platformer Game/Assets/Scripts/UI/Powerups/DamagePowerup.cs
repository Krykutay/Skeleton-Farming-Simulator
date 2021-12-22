using System;
using UnityEngine;

public class DamagePowerup : MonoBehaviour
{
    public static event Action Collected;

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
            // Return to the pool
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // play sound
        Collected?.Invoke();
        //return to the pool
    }
}
