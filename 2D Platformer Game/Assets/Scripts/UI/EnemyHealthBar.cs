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
    float _normalizedHealth;

    Vector2 _workSpace;

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
        edgeRectTransform.gameObject.SetActive(false);
    }

    void Start()
    {
        _currentHealth = _maxHealth;
        _barImage.fillAmount = 1f;
        edgeRectTransform.gameObject.SetActive(false);
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
        edgeRectTransform.gameObject.SetActive(true);

        while (healthAfterHit < _currentHealth)
        {
            _currentHealth -= 0.035f * damageTaken;
            DropHealth();
            yield return new WaitForFixedUpdate();
        }
        _currentHealth = healthAfterHit;
        DropHealth();
    }

    void DropHealth()
    {
        _normalizedHealth = HealthNormalized();
        _barImage.fillAmount = _normalizedHealth;
        _workSpace.Set(_normalizedHealth * _barRectTransform.sizeDelta.x, 0);
        edgeRectTransform.anchoredPosition = _workSpace;
    }
}
