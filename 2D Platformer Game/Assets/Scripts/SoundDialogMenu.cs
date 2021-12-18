using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoundDialogMenu : MonoBehaviour
{
    [Header("Master Volume Setting")]
    [SerializeField] Slider _masterVolumeSlider;
    [SerializeField] TMP_Text _masterVolumeTextValue;
    [SerializeField] float _defaultMasterVolume = 50f;

    [Header("Effect Volume Setting")]
    [SerializeField] Slider _effectVolumeSlider;
    [SerializeField] TMP_Text _effectVolumeTextValue;
    [SerializeField] float _defaultEffectVolume = 100f;

    [Header("Music Volume Setting")]
    [SerializeField] Slider _musicVolumeSlider;
    [SerializeField] TMP_Text _musicVolumeTextValue;
    [SerializeField] float _defaultMusicVolume = 100f;

    [Header("ConfirmationPopup")]
    [SerializeField] GameObject _confirmationPopup;

    [Header("BackButtonActionWithNoWarning")]
    [SerializeField] Button _backBtnActionWithNoWarning;

    float _masterVolume;
    float _effectVolume;
    float _musicVolume;
    bool _isAudioChanged;

    const string MASTER_VOLUME = "masterVolume";
    const string EFFECT_VOLUME = "masterEffect";
    const string MUSIC_VOLUME = "masterMusic";

    void OnEnable()
    {
        LoadPrefs.LoadingDone += LoadPrefs_LoadingDone;
    }

    void OnDisable()
    {
        LoadPrefs.LoadingDone -= LoadPrefs_LoadingDone;
    }

    void Start()
    {
        _masterVolume = _masterVolumeSlider.value;
        _effectVolume = _effectVolumeSlider.value;
        _musicVolume = _musicVolumeSlider.value;
        _isAudioChanged = false;
    }

    void LoadPrefs_LoadingDone()
    {
        _isAudioChanged = false;
    }

    public void SetMasterVolumes(float volume)
    {
        if (AudioListener.volume != volume * 0.01f)
            _isAudioChanged = true;

        AudioListener.volume = volume * 0.01f;
        _masterVolumeTextValue.text = volume.ToString("0");
        _masterVolume = volume;
    }
    
    public void SetEffectVolumes(float volume)
    {
        foreach (var sound in SoundManager.Instance.sounds)
        {
            if (sound.type == SoundManager.SoundTypes.Effect)
            {
                _isAudioChanged = true;
                sound.volume = sound._defaultMaxVolume * volume * 0.01f;
                sound.source.volume = sound._defaultMaxVolume * volume * 0.01f;
                _effectVolume = volume;
            }
        }
        _effectVolumeTextValue.text = volume.ToString();
    }

    public void SetMusicVolumes(float volume)
    {
        foreach (var sound in SoundManager.Instance.sounds)
        {
            if (sound.type == SoundManager.SoundTypes.Music)
            {
                _isAudioChanged = true;
                sound.volume = sound._defaultMaxVolume * volume * 0.01f;
                sound.source.volume = sound._defaultMaxVolume * volume * 0.01f;
                _musicVolume = volume;
            }
        }
        _musicVolumeTextValue.text = volume.ToString("0");
    }

    public void VolumeApply()
    {
        PlayerPrefs.SetFloat(MASTER_VOLUME, _masterVolume);
        PlayerPrefs.SetFloat(EFFECT_VOLUME, _effectVolume);
        PlayerPrefs.SetFloat(MUSIC_VOLUME, _musicVolume);
        _isAudioChanged = false;
    }

    public void ResetButton()
    {
        ResetMasterVolume();
        ResetEffectVolume();
        ResetMusicVolume();

        _isAudioChanged = true;
    }

    public void ResetMasterVolume()
    {
        AudioListener.volume = _defaultMasterVolume * 0.01f;

        _masterVolume = _defaultMasterVolume;

        _masterVolumeSlider.value = _defaultMasterVolume;
        _masterVolumeTextValue.text = _defaultMasterVolume.ToString("0"); 
    }

    public void ResetEffectVolume()
    {
        foreach (var sound in SoundManager.Instance.sounds)
        {
            if (sound.type == SoundManager.SoundTypes.Effect)
            {
                sound.volume = sound._defaultMaxVolume;
                sound.source.volume = sound._defaultMaxVolume;
            }
        }
        _effectVolume = _defaultEffectVolume;

        _effectVolumeSlider.value = _defaultEffectVolume;
        _effectVolumeTextValue.text = _defaultEffectVolume.ToString("0");
    }

    public void ResetMusicVolume()
    {
        foreach (var sound in SoundManager.Instance.sounds)
        {
            if (sound.type == SoundManager.SoundTypes.Music)
            {
                sound.volume = sound._defaultMaxVolume;
                sound.source.volume = sound._defaultMaxVolume;
            }
        }
        _musicVolume = _defaultMusicVolume;

        _musicVolumeSlider.value = _defaultMusicVolume;
        _musicVolumeTextValue.text = _defaultMusicVolume.ToString("0");
    }

    public void BackButton()
    {
        if (_isAudioChanged)
        {
            _confirmationPopup.SetActive(true);
        }
        else
        {
            _backBtnActionWithNoWarning.onClick.Invoke();
        }
    }

    public void DiscardAudioChanges()
    {
        _isAudioChanged = false;
    }

}
