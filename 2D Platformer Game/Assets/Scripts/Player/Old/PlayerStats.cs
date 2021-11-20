using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static Action PlayerDied;

    [SerializeField] float _maxHealth;

    float _currentHealth;

    void OnEnable()
    {
        _currentHealth = _maxHealth;
    }

    public void DecreaseHealth(float amount)
    {
        _currentHealth -= amount;

        if (_currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        DeathChunkParticlePool.Instance.Get(transform.position, Quaternion.Euler(0f, 0f, 0f));
        DeathBloodParticlePool.Instance.Get(transform.position, Quaternion.Euler(0f, 0f, 0f));
        PlayerDied?.Invoke();
        Destroy(gameObject);
    }

}
