using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceHit : MonoBehaviour
{
    float _damageFrequency = 1f;
    float _trigerEnterTime;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        _trigerEnterTime = Time.time;
        Player.Instance.DamageBySurface();
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (Time.time >= _trigerEnterTime + _damageFrequency)
        {
            Player.Instance.DamageBySurface();
            _trigerEnterTime = Time.time;
        }
    }
}
