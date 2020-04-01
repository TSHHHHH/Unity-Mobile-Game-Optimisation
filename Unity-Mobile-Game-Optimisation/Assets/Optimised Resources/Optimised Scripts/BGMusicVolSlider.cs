using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class BGMusicVolSlider : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TextMeshProUGUI volumeText;

    private Slider volumeSilder;

    private void OnEnable()
    {
        volumeSilder = GetComponent<Slider>();

        volumeText.text = (volumeSilder.value * 100f).ToString("F0") + "%";
    }

    public void SetVolumeText(float sliderValue)
    {
        volumeText.text = (sliderValue * 100f).ToString("F0") + "%";
    }
}