using UnityEngine;
using TMPro;

public class HowToPlayPanelControlsDisplay : MonoBehaviour
{
    [SerializeField] TMP_Text _displayedMoveLeftRightKeybindText;
    [SerializeField] TMP_Text _displayedJumpbindText;
    [SerializeField] TMP_Text _displayedAttackKeybindText;
    [SerializeField] TMP_Text _displayedParryKeybindText;
    [SerializeField] TMP_Text _displayedDashKeybindText;
    [SerializeField] TMP_Text _displayedCrouchKeybindText;
    [SerializeField] TMP_Text _displayedInteractKeybindText;

    void OnEnable()
    {
        _displayedMoveLeftRightKeybindText.text = "Left: [" + GameAssets.Instance.keybinds[(int)GameAssets.Keybinds.MoveLeft].text + "] -" +
            " Right: [" + GameAssets.Instance.keybinds[(int)GameAssets.Keybinds.MoveRight].text + "]";

        _displayedJumpbindText.text = "Jump: ["+ GameAssets.Instance.keybinds[(int)GameAssets.Keybinds.Jump].text + "]";
        _displayedAttackKeybindText.text = "Attack: [" + GameAssets.Instance.keybinds[(int)GameAssets.Keybinds.Attack].text + "]";
        _displayedParryKeybindText.text = "Parry: [" + GameAssets.Instance.keybinds[(int)GameAssets.Keybinds.Parry].text + "]";
        _displayedDashKeybindText.text = "Dash: [" + GameAssets.Instance.keybinds[(int)GameAssets.Keybinds.Dash].text + "]";
        _displayedCrouchKeybindText.text = "Crouch: [" + GameAssets.Instance.keybinds[(int)GameAssets.Keybinds.Crouch].text + "]";
        _displayedInteractKeybindText.text = "Interact: [" + GameAssets.Instance.keybinds[(int)GameAssets.Keybinds.Interact].text + "]";
    }
}
