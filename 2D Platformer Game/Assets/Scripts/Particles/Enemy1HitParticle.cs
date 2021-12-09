using UnityEngine;

public class Enemy1HitParticle : MonoBehaviour
{
    [SerializeField] float _animDuration = 0.33f;
    float _animStartTime;

    void OnEnable()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        _animStartTime = Time.time;
    }

    void Update()
    {
        if (Time.time >= _animStartTime + _animDuration)
            Enemy1HitParticlePool.Instance.ReturnToPool(this);
    }
}
