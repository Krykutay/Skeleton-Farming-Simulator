using UnityEngine;

public class HitParticle : MonoBehaviour
{
    void FinishAnim()
    {
        HitParticlePool.Instance.ReturnToPool(this);
    }
}
