using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1f;
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("Mapa");
    }
    /*public void OpenSettings()
	{
		SettingsManager.setActive;
	}*/

    public void Quit()
    {
        Application.Quit();
    }

}
