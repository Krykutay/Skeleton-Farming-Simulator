using UnityEngine;

public class Enemy1HitParticle : MonoBehaviour
{
    void FinishAnim()
    {
        Enemy1HitParticlePool.Instance.ReturnToPool(this);
    }
}
