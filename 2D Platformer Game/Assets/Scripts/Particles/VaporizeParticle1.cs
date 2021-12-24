using UnityEngine;

public class VaporizeParticle1 : MonoBehaviour
{
    ParticleSystem _particleEffect;

    float _particleEffectDuration;
    float _particleEffectStartTime;

    void Awake()
    {
        _particleEffect = GetComponent<ParticleSystem>();
    }

    void OnEnable()
    {
        _particleEffectStartTime = Time.time;
        _particleEffectDuration = _particleEffect.main.duration;

        if (_particleEffect.isPlaying)
            _particleEffect.Stop();

        _particleEffect.Play();
    }

    void Update()
    {
        if (Time.time > (_particleEffectStartTime + _particleEffectDuration))
        {
            VaporizeParticle1Pool.Instance.ReturnToPool(this);
        }
    }
}
