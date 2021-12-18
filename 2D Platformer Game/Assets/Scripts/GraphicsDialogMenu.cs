using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class GraphicsDialogMenu : MonoBehaviour
{
    [SerializeField] TMP_Dropdown _qualityDropdown;
    [SerializeField] Toggle _fullscreenToggle;
    [SerializeField] Toggle _vSyncToggle;

    [Header("Resolution Dropdowns")]
    [SerializeField] TMP_Dropdown _resolutionDropdown;


    [Header("ConfirmationPopup")]
    [SerializeField] GameObject _confirmationPopup;

    [Header("BackButtonActionWithNoWarning")]
    [SerializeField] Button _backBtnActionWithNoWarning;

    int _qualityLevel;
    bool _isFullscreen;
    bool _isvSyncEnabled;
    int _currentResolutionIndex;
    bool _isGraphicsChanged;

    const string MASTER_QUALITY = "masterQuality";
    const string MASTER_FULLSCREEN = "masterFullscreen";
    const string MASTER_VSYNC = "masterVsync";

    Resolution[] _resolutions;
    List<Resolution> _maxHzResolutions;

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
        _maxHzResolutions = new List<Resolution>();
        _resolutions = Screen.resolutions;
        _resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        _currentResolutionIndex = 0;

        int maxHz = 0;
        for (int i = 0; i < _resolutions.Length; i++)
        {
            if (_resolutions[i].refreshRate > maxHz)
            {
                maxHz = _resolutions[i].refreshRate;
            }
        }

        for (int i = 0; i < _resolutions.Length; i++)
        {
            if (_resolutions[i].refreshRate == maxHz)
            {
                Resolution temp = _resolutions[i];
                _maxHzResolutions.Add(temp);

                string option = _resolutions[i].width + " x " + _resolutions[i].height;
                options.Add(option);

                if (_resolutions[i].width == Screen.width && _resolutions[i].height == Screen.height)
                {
                    _currentResolutionIndex = i;
                }
            }
        }

        _resolutionDropdown.AddOptions(options);
        _resolutionDropdown.value = _currentResolutionIndex;
        _resolutionDropdown.RefreshShownValue();

        _qualityLevel = _qualityDropdown.value;
        _isFullscreen = _fullscreenToggle.isOn;
        _isvSyncEnabled = _vSyncToggle.isOn;
        _isGraphicsChanged = false;
    }

    void LoadPrefs_LoadingDone()
    {
        _isGraphicsChanged = false;
    }

    public void SetQuality(int qualityIndex)
    {
        if (_qualityLevel != qualityIndex)
            _isGraphicsChanged = true;
        _qualityLevel = qualityIndex;
        QualitySettings.SetQualityLevel(_qualityLevel);
    }
    public void SetResolution(int resolutionIndex)
    {
        if (_currentResolutionIndex != resolutionIndex)
            _isGraphicsChanged = true;
        Resolution resolution = _maxHzResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        if (_isFullscreen != isFullscreen)
            _isGraphicsChanged = true;
        _isFullscreen = isFullscreen;
        Screen.fullScreen = _isFullscreen;
    }

    public void SetVsync(bool isVsyncEnabled)
    {
        if (_isvSyncEnabled != isVsyncEnabled)
            _isGraphicsChanged = true;
        _isvSyncEnabled = isVsyncEnabled;
        QualitySettings.vSyncCount = (_isvSyncEnabled ? 1 : 0);
    }

    public void GraphicsApply()
    {
        PlayerPrefs.SetInt(MASTER_QUALITY, _qualityLevel);
        
        PlayerPrefs.SetInt(MASTER_FULLSCREEN, (_isFullscreen ? 1 : 0));

        PlayerPrefs.SetInt(MASTER_VSYNC, (_isvSyncEnabled ? 1 : 0));

        _isGraphicsChanged = false;
    }

    public void ResetButton()
    {
        ResetQuality();
        ResetResolution();
        ResetFullscreen();
        ResetvSync();

        _isGraphicsChanged = true;
    }

    public void ResetQuality()
    {
        _qualityLevel = 3;
        _qualityDropdown.value = 3;
        QualitySettings.SetQualityLevel(3);
    }

    public void ResetResolution()   // Disabled on WebGL version
    {
        /*
        Resolution currentResolution = Screen.currentResolution;
        Screen.SetResolution(currentResolution.width, currentResolution.height, Screen.fullScreen);
        _resolutionDropdown.value = _maxHzResolutions.Count;
        */
    }

    public void ResetFullscreen()
    {
        _isFullscreen = false;
        _fullscreenToggle.isOn = false;
        Screen.fullScreen = false;
    }

    public void ResetvSync()
    {
        _isvSyncEnabled = true;
        _vSyncToggle.isOn = true;
        QualitySettings.vSyncCount = 1;
    }

    public void BackButton()
    {
        if (_isGraphicsChanged)
        {
            _confirmationPopup.SetActive(true);
        }
        else
        {
            _backBtnActionWithNoWarning.onClick.Invoke();
        }
    }

    public void DiscardGraphicsChanges()
    {
        _isGraphicsChanged = false;
    }

}

