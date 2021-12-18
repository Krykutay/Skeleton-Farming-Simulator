using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class EscToGoBackInMenu : MonoBehaviour
{
    [SerializeField] Button _backButton;

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            EscToGoBack();
        }
    }

    public void EscToGoBack()
    {
        if(_backButton.gameObject.activeSelf)
            _backButton.onClick.Invoke();
    }
}
