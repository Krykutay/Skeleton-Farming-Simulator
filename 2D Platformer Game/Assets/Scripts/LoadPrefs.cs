using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using System;

public class LoadPrefs : MonoBehaviour
{
    [Header("General Settings")]
    GraphicsDialogMenu _graphicsDialogMenu;
    SoundDialogMenu _soundDialogMenu;

    [Header("Audio Setting")]
    [SerializeField] Slider _masterVolumeSlider;
    [SerializeField] TMP_Text _masterVolumeTextValue;
    [SerializeField] Slider _effectVolumeSlider;
    [SerializeField] TMP_Text _effectVolumeTextValue;
    [SerializeField] Slider _musicVolumeSlider;
    [SerializeField] TMP_Text _musicVolumeTextValue;

    [Header("Quality Level Setting")]
    [SerializeField] TMP_Dropdown _qualityDropdown;

    [Header("Fullscreen Setting")]
    [SerializeField] Toggle _fullscreenToggle;

    [Header("Vsync Setting")]
    [SerializeField] Toggle _vSyncToggle;

    [Header("Controls Setting")]
    [SerializeField] InputActionAsset _actions;
    [SerializeField] TMP_Text _jumpControlsTextValue;
    [SerializeField] TMP_Text _fireControlsTextValue;

    const string REBINDS = "rebinds";

    const string MASTER_VOLUME = "masterVolume";
    const string EFFECT_VOLUME = "masterEffect";
    const string MUSIC_VOLUME = "masterMusic";

    const string MASTER_QUALITY = "masterQuality";
    const string MASTER_FULLSCREEN = "masterFullscreen";
    const string MASTER_VSYNC = "masterVsync";

    public static Action LoadingDone;

    void Awake()
    {
        _graphicsDialogMenu = GetComponent<GraphicsDialogMenu>();
        _soundDialogMenu = GetComponent<SoundDialogMenu>();
    }

    void Start()
    {
        LoadControls();
        LoadVolumes();
        LoadGraphics();
        LoadingDone?.Invoke();
    }

    public void LoadVolumes()
    {
        LoadMasterVolume();
        LoadEffectVolume();
        LoadMusicVolume();
    }

    public void LoadGraphics()
    {
        LoadQualityGraphics();
        LoadFullscreenGraphics();
        LoadvSyncGraphics();
    }


    public void LoadControls()
    {
        var rebinds = PlayerPrefs.GetString(REBINDS);
        if (!string.IsNullOrEmpty(rebinds))
        {
            _jumpControlsTextValue.text = InputControlPath.ToHumanReadableString(
                _actions.actionMaps[0].actions[0].bindings[0].effectivePath,
                InputControlPath.HumanReadableStringOptions.OmitDevice);

            _fireControlsTextValue.text = InputControlPath.ToHumanReadableString(
                _actions.actionMaps[0].actions[1].bindings[0].effectivePath,
                InputControlPath.HumanReadableStringOptions.OmitDevice);
        }  
        else
        {
            _jumpControlsTextValue.text = "Left Button";
            _fireControlsTextValue.text = "Space";
            
            foreach (InputActionMap map in _actions.actionMaps)
            {
                map.RemoveAllBindingOverrides();
            }
        }
    }

    void LoadMasterVolume()
    {
        if (PlayerPrefs.HasKey(MASTER_VOLUME))
        {
            float localVolume = PlayerPrefs.GetFloat(MASTER_VOLUME);

            AudioListener.volume = localVolume * 0.01f;
            _masterVolumeSlider.value = localVolume;
            _masterVolumeTextValue.text = localVolume.ToString();
        }
        else
        {
            _soundDialogMenu.ResetMasterVolume();
        }
    }

    void LoadEffectVolume()
    {
        if (PlayerPrefs.HasKey(EFFECT_VOLUME))
        {
            float localVolume = PlayerPrefs.GetFloat(EFFECT_VOLUME);
            foreach (var sound in SoundManager.Instance.sounds)
            {
                if (sound.type == SoundManager.SoundTypes.Effect)
                {
                    sound.volume = sound._defaultMaxVolume * localVolume * 0.01f;
                    sound.source.volume = sound._defaultMaxVolume * localVolume * 0.01f;
                }
            }
            _effectVolumeSlider.value = localVolume;
            _effectVolumeTextValue.text = localVolume.ToString();
        }
        else
        {
            _soundDialogMenu.ResetEffectVolume();
        }
    }

    void LoadMusicVolume()
    {
        if (PlayerPrefs.HasKey(MUSIC_VOLUME))
        {
            float localVolume = PlayerPrefs.GetFloat(MUSIC_VOLUME);
            foreach (var sound in SoundManager.Instance.sounds)
            {
                if (sound.type == SoundManager.SoundTypes.Music)
                {
                    sound.volume = sound._defaultMaxVolume * localVolume * 0.01f;
                    sound.source.volume = sound._defaultMaxVolume * localVolume * 0.01f;
                }
            }
            _musicVolumeSlider.value = localVolume;
            _musicVolumeTextValue.text = localVolume.ToString();
        }
        else
        {
            _soundDialogMenu.ResetMusicVolume();
        }
    }

    void LoadQualityGraphics()
    {
        if (PlayerPrefs.HasKey(MASTER_QUALITY))
        {
            int localQuality = PlayerPrefs.GetInt(MASTER_QUALITY);
            _qualityDropdown.value = localQuality;
            QualitySettings.SetQualityLevel(localQuality);
        }
        else
        {
            _graphicsDialogMenu.ResetQuality();
        }
    }

    void LoadFullscreenGraphics()
    {
        if (PlayerPrefs.HasKey(MASTER_FULLSCREEN))
        {
            int localFullscreen = PlayerPrefs.GetInt(MASTER_FULLSCREEN);

            if (localFullscreen == 1)
            {
                Screen.fullScreen = true;
                _fullscreenToggle.isOn = true;
            }
            else
            {
                Screen.fullScreen = false;
                _fullscreenToggle.isOn = false;
            }
        }
        else
        {
            _graphicsDialogMenu.ResetFullscreen();
        }
    }

    void LoadvSyncGraphics()
    {
        if (PlayerPrefs.HasKey(MASTER_VSYNC))
        {
            int localVsync = PlayerPrefs.GetInt(MASTER_VSYNC);

            if (localVsync == 1)
            {
                QualitySettings.vSyncCount = 1;
                _vSyncToggle.isOn = true;
            }
            else
            {
                QualitySettings.vSyncCount = 0;
                _vSyncToggle.isOn = false;
            }
        }
        else
        {
            _graphicsDialogMenu.ResetvSync();
        }
    }

}
