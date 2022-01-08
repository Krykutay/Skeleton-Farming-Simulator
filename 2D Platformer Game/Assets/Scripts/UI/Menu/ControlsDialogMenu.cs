using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class ControlsDialogMenu : MonoBehaviour
{
    public static TMP_Text[] KeyBinds { get { return _bindingDisplayNameTexts; } }

    [SerializeField] InputActionAsset _actions;
    [SerializeField] InputActionReference[] _actionReferences;
    [SerializeField] static TMP_Text[] _bindingDisplayNameTexts;
    [SerializeField] GameObject[] _waitingForInputPanels;

    [Header("ConfirmationPopup")]
    [SerializeField] GameObject _confirmationPopup;

    [Header("ConfirmationPopupYesBtn")]
    [SerializeField] Button _backBtnActionWithNoWarning;

    InputActionRebindingExtensions.RebindingOperation _rebindingOperation;

    bool _hasControlsChanged;
    int _keybindIndex;

    const int TOTAL_BINDINGS_COUNT = 8;

    void OnDisable()
    {
        if (_waitingForInputPanels[_keybindIndex].activeSelf)
        {
            _waitingForInputPanels[_keybindIndex].SetActive(false);
            _actions.Enable();
            _rebindingOperation.Dispose();
        }
    }

    public void AssignKeybinding(int keybindIndex)  // 0 for jump, 1 for Fire
    {
        _keybindIndex = keybindIndex;
    }

    public void StartRebinding()
    {
        _waitingForInputPanels[_keybindIndex].SetActive(true);

        //int bindingIndex = _actionReference.action.GetBindingIndexForControl(_actionReference.action.controls[0]);
        int bindingIndex = 0;

        _actions.Disable();

        _rebindingOperation = _actionReferences[_keybindIndex].action.PerformInteractiveRebinding()
            .WithCancelingThrough("<Keyboard>/escape")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation =>
            {
                
                if (CheckDuplicateBindings(_actionReferences[_keybindIndex].action, bindingIndex))
                {
                    _actionReferences[_keybindIndex].action.RemoveBindingOverride(bindingIndex);
                    _rebindingOperation.Dispose();
                    _rebindingOperation = null;
                    StartRebinding();
                    return;
                } 
                RebindComplete(bindingIndex);
            })
            .OnCancel(operation => {
                _waitingForInputPanels[_keybindIndex].SetActive(false);
                _actions.Enable();
                _rebindingOperation.Dispose();
            })
            .Start();
    }

    void RebindComplete(int bindingIndex)
    {
        _bindingDisplayNameTexts[_keybindIndex].text = GetBindingDisplayName(_actionReferences[_keybindIndex].action, bindingIndex); ;

        _rebindingOperation.Dispose();

        _waitingForInputPanels[_keybindIndex].SetActive(false);
        _hasControlsChanged = true;

        _actions.Enable();

    }

    bool CheckDuplicateBindings(InputAction action, int bindingIndex)
    {
        InputBinding newBinding = action.bindings[bindingIndex];
        foreach (InputBinding binding in action.actionMap.bindings)
        {
            if (binding.action == newBinding.action)
            {
                continue;
            }
            if (binding.effectivePath == newBinding.effectivePath)
            {
                //Debug.Log("Duplicate binding found: " + newBinding.effectivePath);
                return true;
            }
        }
        return false;
    }

    public void ResetAllToDefaultButton()
    {
        foreach (InputActionMap map in _actions.actionMaps)
        {
            map.RemoveAllBindingOverrides();
        }

        int bindingIndex = 0;

        for (int i = 0; i < TOTAL_BINDINGS_COUNT; i++)
        {
            _bindingDisplayNameTexts[i].text = GetBindingDisplayName(_actions.actionMaps[0].actions[i], bindingIndex);
        }

        _hasControlsChanged = true;
    }

    public void ResetButton(int keybindIndex)
    {
        int bindingIndex = _actionReferences[_keybindIndex].action.GetBindingIndexForControl(_actionReferences[_keybindIndex].action.controls[0]);

        // Check for duplicate bindings before resetting to default, and if found, swap the two controls.
        if (SwapResetBindings(_actionReferences[keybindIndex].action, bindingIndex, keybindIndex))
        {
            return;
        }

        _actionReferences[keybindIndex].action.RemoveAllBindingOverrides();

        _bindingDisplayNameTexts[keybindIndex].text = GetBindingDisplayName(_actionReferences[keybindIndex].action, bindingIndex);

        _hasControlsChanged = true;
    }

    private bool SwapResetBindings(InputAction action, int bindingIndex, int keybindIndex)
    {
        // Cache a reference to the current binding.
        InputBinding newBinding = action.bindings[bindingIndex];
        // Check all of the bindings in the current action map to make sure there are no duplicates.
        for (int i = 0; i < TOTAL_BINDINGS_COUNT; i++)
        {
            //InputBinding binding = action.actionMap.bindings[i];
            InputBinding binding = _actions.actionMaps[0].actions[i].bindings[bindingIndex];

            if (binding.action == newBinding.action)
            {
                continue;
            }
            if (binding.effectivePath == newBinding.path)
            {
                //Debug.Log("Duplicate binding found for reset to default: " + newBinding.effectivePath);
                // Swap the two actions.
                
                _bindingDisplayNameTexts[i].text = GetBindingDisplayName(_actionReferences[keybindIndex].action, bindingIndex);

                action.actionMap.FindAction(binding.action).ApplyBindingOverride(0, newBinding.overridePath);
                action.RemoveBindingOverride(bindingIndex);

                _bindingDisplayNameTexts[keybindIndex].text = GetBindingDisplayName(_actionReferences[keybindIndex].action, bindingIndex);

                _hasControlsChanged = true;
                return true;
            }
        }
        return false;
    }

    string GetBindingDisplayName(InputAction action, int bindingIndex)
    {
        return InputControlPath.ToHumanReadableString(
                action.bindings[bindingIndex].effectivePath,
                InputControlPath.HumanReadableStringOptions.OmitDevice);
    }

    public void ControlsApply()
    {
        
        var rebinds = _actions.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString("rebinds", rebinds);

        
        if (!string.IsNullOrEmpty(rebinds))
        {
            _actions.LoadBindingOverridesFromJson(rebinds);
        }
        else
        {
            foreach (InputActionMap map in _actions.actionMaps)
            {
                map.RemoveAllBindingOverrides();
            }
        } 

        _hasControlsChanged = false;
    }


    public void BackButton()
    {
        if (_hasControlsChanged)
        {
            _confirmationPopup.SetActive(true);
        }
        else
        {
            _backBtnActionWithNoWarning.onClick.Invoke();
        }
    }

    public void DiscardControlsChanges()
    {
        _hasControlsChanged = false;
    }
}
