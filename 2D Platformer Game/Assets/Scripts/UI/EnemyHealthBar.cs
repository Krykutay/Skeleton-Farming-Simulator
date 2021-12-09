using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    Image _barImage;
    RectTransform _barRectTransform;
    RectTransform edgeRectTransform;

    float _maxHealth;
    float _currentHealth;

    void Awake()
    {
        _barImage = transform.Find("bar").GetComponent<Image>();
        edgeRectTransform = transform.Find("edge").GetComponent<RectTransform>();
        _barRectTransform = _barImage.GetComponent<RectTransform>();
    }

    void OnEnable()
    {
        _currentHealth = _maxHealth;
        _barImage.fillAmount = HealthNormalized();
        edgeRectTransform.gameObject.SetActive(HealthNormalized() < 1f);
    }

    void Start()
    {
        _barImage.fillAmount = 1f;
        edgeRectTransform.gameObject.SetActive(HealthNormalized() < 1f);
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
        float normalizedHealth;

        while (healthAfterHit < _currentHealth)
        {
            normalizedHealth = HealthNormalized();
            _currentHealth -= 0.04f * damageTaken;
            _barImage.fillAmount = normalizedHealth;
            edgeRectTransform.anchoredPosition = new Vector2(HealthNormalized() * _barRectTransform.sizeDelta.x, 0);
            yield return new WaitForFixedUpdate();
        }
        _currentHealth = healthAfterHit;
        _barImage.fillAmount = HealthNormalized();
        edgeRectTransform.anchoredPosition = new Vector2(HealthNormalized() * _barRectTransform.sizeDelta.x, 0);
        edgeRectTransform.gameObject.SetActive(_currentHealth < _maxHealth);
    }
}
