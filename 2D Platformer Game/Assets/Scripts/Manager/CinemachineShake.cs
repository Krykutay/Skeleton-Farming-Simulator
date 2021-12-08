using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance;

    CinemachineVirtualCamera _cvc;
    CinemachineBasicMultiChannelPerlin _cPerlin;

    float _shakeTimer;
    float _shakeTimerTotal;
    float _startingIntensity;

    void Awake()
    {
        Instance = this;
        _cvc = GetComponent<CinemachineVirtualCamera>();
        _cPerlin = _cvc.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        _cPerlin.m_AmplitudeGain = intensity;

        _startingIntensity = intensity;
        _shakeTimer = time;
        _shakeTimerTotal = time;
    }

    void Update()
    {
        if (_shakeTimer > 0)
        {
            _shakeTimer -= Time.deltaTime;

            if (_shakeTimer <= 0f)
            {
                _cPerlin.m_AmplitudeGain = 0f;
                Mathf.Lerp(_startingIntensity, 0f, 1f - (_shakeTimer / _shakeTimerTotal));
            }
        }
    }
}
