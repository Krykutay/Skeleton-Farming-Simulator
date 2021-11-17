using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitParticleController : MonoBehaviour
{
    void FinishAnim()
    {
        HitParticlePool.Instance.ReturnToPool(this);
    }
}
