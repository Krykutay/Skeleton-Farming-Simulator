using UnityEngine;

public class BirdIdle : MonoBehaviour, IDamageable
{
    public bool Damage(AttackDetails attackDetails)
    {
        DeathBloodParticlePool.Instance.Get(transform.position, Quaternion.identity);
        DeathChunkParticlePool.Instance.Get(transform.position, Quaternion.identity);
        
        Destroy(gameObject);

        return true;
    }
}
