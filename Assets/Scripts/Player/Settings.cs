using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider; // Suwak do głośności
    [SerializeField] private Slider brightnessSlider; // Suwak do jasności

    private void Start()
    {
        // Załaduj ustawienia przy starcie
        LoadSettings();

        // Dodaj listener do suwaków
        volumeSlider.onValueChanged.AddListener(SetVolume);
        brightnessSlider.onValueChanged.AddListener(SetBrightness);
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        PlayerPrefs.SetFloat("Brightness", brightnessSlider.value);
        PlayerPrefs.Save();
    }

    private void LoadSettings()
    {
        if (PlayerPrefs.HasKey("Volume"))
        {
            volumeSlider.value = PlayerPrefs.GetFloat("Volume");
            SetVolume(volumeSlider.value);
        }

        if (PlayerPrefs.HasKey("Brightness"))
        {
            brightnessSlider.value = PlayerPrefs.GetFloat("Brightness");
            SetBrightness(brightnessSlider.value);
        }
    }

    private void SetVolume(float volume)
    {
        AudioListener.volume = volume; // Ustaw głośność w AudioListener
    }

    private void SetBrightness(float brightness)
    {
        // Ustaw jasność (możesz to dostosować do swojego systemu oświetlenia)
        // Przykład: zmiana jasności materiału
        // RenderSettings.ambientLight = Color.white * brightness;
    }

    private void OnDestroy()
    {
        // Zapisz ustawienia przy zniszczeniu obiektu
        SaveSettings();
    }
}