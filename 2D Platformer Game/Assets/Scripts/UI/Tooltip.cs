using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class Tooltip : MonoBehaviour
{
    TextMeshProUGUI _textMeshPro;
    RectTransform _backgroundRectTransform;
    RectTransform _rectTransform;

    void Awake()
    {
        _backgroundRectTransform = transform.Find("background").GetComponent<RectTransform>();
        _textMeshPro = transform.Find("text").GetComponent<TextMeshProUGUI>();
        _rectTransform = GetComponent<RectTransform>();

        ShowTooltip("Random tooltip text");
    }

    void Update()
    {
        //_rectTransform.anchoredPosition = Mouse.current.position.ReadValue();
    }

    void ShowTooltip(string tooltipString)
    {
        gameObject.SetActive(true);

        _textMeshPro.SetText(tooltipString);
        _textMeshPro.ForceMeshUpdate();

        Vector2 textSize = _textMeshPro.GetRenderedValues(false);
        Vector2 paddingSize = new Vector2(10, 10);
        _backgroundRectTransform.sizeDelta = textSize + paddingSize;
    }

    void HideTooltip()
    {
        gameObject.SetActive(false);
    }

}
