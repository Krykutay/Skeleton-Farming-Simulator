using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    Image _barImage;

    float _maxHealth;
    float _currentHealth;

    void Awake()
    {
        _barImage = transform.Find("bar").GetComponent<Image>();
    }

    void OnEnable()
    {
        _currentHealth = _maxHealth;
        _barImage.fillAmount = HealthNormalized();
    }

    void Start()
    {
        _barImage.fillAmount = 1f;
    }

    float HealthNormalized() => _currentHealth / _maxHealth;

    public void SetMaxHealth(int maxHealth) => _maxHealth = (float)maxHealth;

    public void SetCurrentHealth(int currentHealth, int damageTaken)
    {
        if (currentHealth != _maxHealth)
            StartCoroutine(DropHealthSmoothly(currentHealth, damageTaken));
    }

    IEnumerator DropHealthSmoothly(int healthAfterHit, int damageTaken)
    {
        while (healthAfterHit < _currentHealth)
        {
            _currentHealth -= 0.04f * damageTaken;
            _barImage.fillAmount = HealthNormalized();
            yield return new WaitForFixedUpdate();
        }
        _currentHealth = healthAfterHit;
        _barImage.fillAmount = HealthNormalized();
    }
}
