using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    static int _sortingOrder;

    TextMeshPro _textMesh;
    Color _textColor;
    Color _initialTextColor;
    Vector3 _moveVector;

    float _disappearTimer;

    const float DISAPPEAR_TIMER_MAX = 1f;

    void Awake()
    {
        _textMesh = transform.GetComponent<TextMeshPro>();
        _initialTextColor = _textMesh.color;
    }

    void OnEnable()
    {
        _textMesh.color = _initialTextColor;
    }

    public void Setup(int damageAmount, bool isCriticalHit)
    {
        _textMesh.SetText(damageAmount.ToString());

        if (!isCriticalHit)
        {
            _textMesh.fontSize = 5;
            _textMesh.color = new Color(1, 197f / 255, 0, 1);
        }
        else
        {
            _textMesh.fontSize = 6;
            _textMesh.color = new Color(1, 43f / 255, 0, 1);
        }

        _textColor = _textMesh.color;
        _disappearTimer = DISAPPEAR_TIMER_MAX;

        _sortingOrder++;
        _textMesh.sortingOrder = _sortingOrder;

        _moveVector.Set(0.7f, 1f, 0);
    }

    void Update()
    {
        transform.position += _moveVector * Time.deltaTime;
        _moveVector -= _moveVector * 0.15f * Time.deltaTime;

        if (_disappearTimer > DISAPPEAR_TIMER_MAX * 0.5f)
        {
            float increaseScaleAmount = 0.7f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        }
        else
        {
            float decreaseScaleAmount = 0.7f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }

        _disappearTimer -= Time.deltaTime;
        if (_disappearTimer < 0)
        {
            // Start disappear
            float disappearSpeed = 3f;
            _textColor.a -= disappearSpeed * Time.deltaTime;
            _textMesh.color = _textColor;

            if (_textColor.a < 0)
            {
                transform.localScale = Vector3.one;
                DamagePopupPool.Instance.ReturnToPool(this);
            }
        }
    }
}
