using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public void PlayGame()
	{
		SceneManager.LoadSceneAsync("Mapa");
	}
	public void OpenSettings()
	{
		SettingsManager.SetActive(true);
	}

	public void Quit()
	{
		Application.Quit();
	}

}
