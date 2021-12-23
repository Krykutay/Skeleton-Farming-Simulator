using UnityEngine;
using UnityEngine.UI;

public class PowerupCountdownBar : MonoBehaviour
{
    Image _barImage;

    float _duration;
    float _remaningDuration;

    void Awake()
    {
        _barImage = transform.Find("bar").GetComponent<Image>();
    }

    void Update()
    {
        if (_remaningDuration <= 0)
        {
            return;
        }
        _remaningDuration -= Time.deltaTime;
        _barImage.fillAmount = GetDurationNormalized();
    }
    
    float GetDurationNormalized()
    {
        return _remaningDuration / _duration;
    }

    public void StartCountdown(float duration)
    {
        _duration = duration;
        _remaningDuration = duration;
    }
}
