using UnityEngine;
using UnityEngine.UI;

public class EscToGoBackInMenu : MonoBehaviour
{
    [SerializeField] Button _backButton;

    void OnEnable()
    {
        InputManager.Instance.OnPauseAction += InputManager_PausePressed;
    }

    void OnDisable()
    {
       InputManager.Instance.OnPauseAction -= InputManager_PausePressed; 
    }

    void InputManager_PausePressed()
    {
        EscToGoBack();
    }

    public void EscToGoBack()
    {
        if(_backButton.gameObject.activeSelf)
            _backButton.onClick.Invoke();
    }
}
