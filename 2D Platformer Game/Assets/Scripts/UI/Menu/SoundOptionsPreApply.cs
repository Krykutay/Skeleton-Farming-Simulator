using UnityEngine;
using UnityEngine.UI;

public class SoundOptionsPreApply : MonoBehaviour
{
    [SerializeField] Button _applyButton;

    void Start()
    {
        _applyButton.onClick.Invoke();
    }
}
