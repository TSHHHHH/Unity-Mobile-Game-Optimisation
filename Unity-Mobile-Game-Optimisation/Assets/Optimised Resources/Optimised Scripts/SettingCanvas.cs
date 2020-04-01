using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingCanvas : MonoBehaviour
{
    public GameObject options;
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public GameObject FPSPanel;

    public bool FPSCounterToggle;
    public bool currentFPSCounterToggle;

    public float musicVolume;
    public float currentMusicVolume;

    public AudioMixer audioMixer;
    public TextMeshProUGUI volumeText;

    public Slider volumeSilder;
    public Toggle FPSToggle;

    public void Start()
    {
        if (SceneManager.GetActiveScene().name == "Start Menu")
        {
            FPSCounterToggle = true;
            currentFPSCounterToggle = FPSCounterToggle;

            musicVolume = 1;
            currentMusicVolume = musicVolume;

            SaveSystem.saveSetting(this);
        }
        else
        {
            FPSPanel.SetActive(SaveSystem.LoadSettingData().FPSCounterToggle);
        }
    }

    public void OpenSettings()
    {
        if (options != null)
        {
            options.SetActive(false);
        }
        else if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }

        currentFPSCounterToggle = FPSCounterToggle;
        FPSToggle.isOn = FPSCounterToggle;

        currentMusicVolume = musicVolume;
        volumeSilder.value = musicVolume;

        settingsMenu.SetActive(true);
    }

    public void SetLevel(float sliderValue)
    {
        audioMixer.SetFloat("BGMusicVol", Mathf.Log10(sliderValue) * 20f);

        musicVolume = sliderValue;
    }

    public void SetVolumeText(float sliderValue)
    {
        volumeText.text = (sliderValue * 100f).ToString("F0") + "%";
    }

    public void ToggleFPSPanel(bool isToggeled)
    {
        if (SceneManager.GetActiveScene().name != "Start Menu")
        {
            FPSPanel.SetActive(isToggeled);
        }

        FPSCounterToggle = isToggeled;
    }

    public void ApplySetting()
    {        
        if (SceneManager.GetActiveScene().name == "Start Menu")
        {
            SaveSystem.saveSetting(this);

            settingsMenu.SetActive(false);

            options.SetActive(true);
        }
        else
        {
            SaveSystem.saveSetting(this);

            settingsMenu.SetActive(false);

            pauseMenu.SetActive(true);

            FPSPanel.SetActive(SaveSystem.LoadSettingData().FPSCounterToggle);
        }
    }

    public void back()
    {
        musicVolume = currentMusicVolume;
        volumeSilder.value = currentMusicVolume;

        FPSCounterToggle = currentFPSCounterToggle;
        FPSToggle.isOn = currentFPSCounterToggle;

        settingsMenu.SetActive(false);

        if(options != null)
        {
            options.SetActive(true);
        }
    }

    public void LoadSetting()
    {
        GameData data = SaveSystem.LoadSettingData();

        FPSCounterToggle = data.FPSCounterToggle;
        musicVolume = data.musicVolume;
    }
}