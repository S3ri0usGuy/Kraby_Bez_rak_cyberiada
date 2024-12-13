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
        else
        {
            // Ustaw domyślną wartość głośności, jeśli nie została wcześniej ustawiona
            volumeSlider.value = 1.0f; // 100% głośności
            SetVolume(volumeSlider.value);
        }

        if (PlayerPrefs.HasKey("Brightness"))
        {
            brightnessSlider.value = PlayerPrefs.GetFloat("Brightness");
            SetBrightness(brightnessSlider.value);
        }
        else
        {
            // Ustaw domyślną wartość jasności, jeśli nie została wcześniej ustawiona
            brightnessSlider.value = 1.0f; // 100% jasności
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
        RenderSettings.ambientLight = Color.white * brightness; // Ustaw jasność otoczenia
    }

    private void OnDestroy()
    {
        // Zapisz ustawienia przy zniszczeniu obiektu
        SaveSettings();
    }
}