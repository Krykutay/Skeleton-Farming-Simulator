using UnityEngine;
using TMPro;

public class HowToPlayPanelControlsDisplay : MonoBehaviour
{
    [SerializeField] TMP_Text _currentJumpKeybindText;
    [SerializeField] TMP_Text _currentFireKeybindText;

    [SerializeField] TMP_Text _displayedJumpKeybindText;
    [SerializeField] TMP_Text _displayedFireKeybindText;

    void OnEnable()
    {
        _displayedJumpKeybindText.text = "Jump/Fly: [" + _currentJumpKeybindText.text + "]";
        _displayedFireKeybindText.text = "Fire: [" + _currentFireKeybindText.text + "]";
    }
}
