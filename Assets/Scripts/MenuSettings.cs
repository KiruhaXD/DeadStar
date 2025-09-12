using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuSettings : MonoBehaviour
{
    [SerializeField] GameObject _settingsPanel;

    [SerializeField] VolumeController[] _volumesController;
    [SerializeField] MouseSensivity _mouseSensivity;

    public GameObject graphicsPanel;
    public GameObject audioPanel;
    public GameObject keybinPanel;

    [SerializeField] TMP_Dropdown _resolutuionDisplay;
    [SerializeField] TMP_Dropdown _fullScreenResolution;
    [SerializeField] TMP_Dropdown _qualityDropdown;

    Resolution[] resolutions;

    private List<ScreenMode> _screenModes = new()
    {
        new ScreenMode("Full Screen", FullScreenMode.FullScreenWindow),
        new ScreenMode("Windowed", FullScreenMode.Windowed),
        new ScreenMode("Maximized Windowed", FullScreenMode.MaximizedWindow)
    };

    private void Start()
    {
        _resolutuionDisplay.ClearOptions();
        List<string> options = new List<string>();
        resolutions = Screen.resolutions;
        int currentResolutioIndex = 0;

        for (int i = 0; i < resolutions.Length; i++) 
        {
            string option = resolutions[i].width + "x" + resolutions[i].height + " " + resolutions[i].refreshRateRatio + "Hz";
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                currentResolutioIndex = i;
        }

        _resolutuionDisplay.AddOptions(options);
        _resolutuionDisplay.RefreshShownValue();

        if(SceneManager.GetActiveScene().name == "SampleScene")
            LoadSettingSensivity();

        LoadSettings(currentResolutioIndex);
    }

    private void Update()
    {
        if (_settingsPanel.activeSelf == true) 
        {
            if (Input.GetKeyDown(KeyCode.Space))
                SaveSettings();

            else if (Input.GetKeyDown(KeyCode.Escape) && _settingsPanel.name == "Panel (SettingsInGameMenu)")
                _settingsPanel.SetActive(false);
        }
    }

    public void ResolutionDisplay(int resolutionIndex) 
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void FullScreenDisplay(int index) 
    {
        FullScreenMode mode = (FullScreenMode)index;
        Screen.fullScreenMode = mode;
    }

    public void Quality(int index) 
    {
        QualitySettings.SetQualityLevel(index);
    }

    public void SaveResolutionDisplay() 
    {
        List<Resolution> resolutions = Screen.resolutions.ToList();

        Resolution resolution = resolutions.Find(x => x.ToString() == _resolutuionDisplay.options[_resolutuionDisplay.value].text);
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode);
    }

    public void SaveFullScreenDisplay() 
    {
        FullScreenMode fullScreenMode = _screenModes.Find(x => x.Name == _fullScreenResolution.options[_fullScreenResolution.value].text).FullscreenMode;
        Screen.SetResolution(Screen.width, Screen.height, fullScreenMode);
    }

    public void SaveSettings() 
    {
        PlayerPrefs.SetInt("QualitySettingPreference", _qualityDropdown.value);
        PlayerPrefs.SetInt("ResolutionPreference", _resolutuionDisplay.value);
        PlayerPrefs.SetInt("FullScreenPreference", _fullScreenResolution.value);

        for (int i = 0; i < _volumesController.Length; i++) 
        {
            PlayerPrefs.SetFloat(_volumesController[i].volumeParameters[i], _volumesController[i]._volumeValue);
        }

        for (int i = 0; i < _mouseSensivity.mouseSlidersSensivity.Length; i++) 
        {
            PlayerPrefs.SetFloat("Sensivity", _mouseSensivity.mouseSlidersSensivity[i].value);
        }


        Debug.Log("ÑÎÕÐÀÍÅÍÈÅ ÄÀÍÍÛÕ(ÇÂÓÊÀ È ÃÐÀÔÈÊÈ È ×ÓÂÑÒÂÈÒÅËÜÍÎÑÒÈ ÌÛØÈ) ÏÐÎÈÇÎØËÎ ÓÑÏÅØÍÎ");

    }

    public void LoadSettingSensivity()
    {
        for (int i = 0; i < _mouseSensivity.mouseSlidersSensivity.Length; i++)
        {
            _mouseSensivity.mouseSlidersSensivity[i].value = (_mouseSensivity.sensivity - _mouseSensivity._minSensivity) / (_mouseSensivity._maxSensivity - _mouseSensivity._minSensivity);

            if (PlayerPrefs.HasKey("Sensivity"))
            {
                _mouseSensivity.mouseSlidersSensivity[i].value = PlayerPrefs.GetFloat("Sensivity");
            }
        }

        Debug.Log("Çàãðóçêà íàñòðîåê ÷óâñòâèòåëüíîñòè");
    }

    public void LoadSettings(int currentResolutionIndex) 
    {
        if (PlayerPrefs.HasKey("QualitySettingPreference"))
            _qualityDropdown.value = PlayerPrefs.GetInt("QualitySettingPreference");
        else
            _qualityDropdown.value = 3;

        if (PlayerPrefs.HasKey("ResolutionPreference"))
            _resolutuionDisplay.value = PlayerPrefs.GetInt("ResolutionPreference");
        else
            _resolutuionDisplay.value = currentResolutionIndex;

        if (PlayerPrefs.HasKey("FullScreenPreference"))
            _fullScreenResolution.value = PlayerPrefs.GetInt("FullScreenPreference");
        else
            _fullScreenResolution.value = 3;

        for (int i = 0; i < _volumesController.Length; i++)
        {
            if (PlayerPrefs.HasKey(_volumesController[i].volumeParameters[i]))
            {
                _volumesController[i]._volumeValue = PlayerPrefs.GetFloat(_volumesController[i].volumeParameters[i], Mathf.Log10(_volumesController[i].volumeSlider.value) * _volumesController[i]._multiplier);
                _volumesController[i].volumeSlider.value = Mathf.Pow(10f, _volumesController[i]._volumeValue / _volumesController[i]._multiplier);
            }

        }
    }

    public void GraphicsButton() 
    {
        graphicsPanel.SetActive(true);
        audioPanel.SetActive(false);
        keybinPanel.SetActive(false);
    }

    public void AudioButton() 
    {
        audioPanel.SetActive(true);
        graphicsPanel.SetActive(false);
        keybinPanel.SetActive(false);
    }

    public void KeyBindingsButton() 
    {
        keybinPanel.SetActive(true);
        audioPanel.SetActive(false);
        graphicsPanel.SetActive(false);
    }
}
