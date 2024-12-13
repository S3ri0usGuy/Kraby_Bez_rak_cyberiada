using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider; // Suwak do głośności
    [SerializeField] private Slider brightnessSlider; // Suwak do jasności
    [SerializeField] private GameObject settingsPanel; // Panel ustawień

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
            volumeSlider.value = 1.0f; // Domyślna wartość głośności
            SetVolume(volumeSlider.value);
        }

        if (PlayerPrefs.HasKey("Brightness"))
        {
            brightnessSlider.value = PlayerPrefs.GetFloat("Brightness");
            SetBrightness(brightnessSlider.value);
        }
        else
        {
            brightnessSlider.value = 1.0f; // Domyślna wartość jasności
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

    public void OpenSettings()
    {
        settingsPanel.SetActive(true); // Włącz panel ustawień
    }

    public void CloseSettings()
    {
        SaveSettings(); // Zapisz ustawienia przy zamykaniu panelu
        settingsPanel.SetActive(false); // Wyłącz panel ustawień
    }

    private void OnDestroy()
    {
        SaveSettings(); // Zapisz ustawienia przy zniszczeniu obiektu
    }
}