using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class winCondition : MonoBehaviour
{
    private bool won = false;
    private InputProvider _inputProvider;

    public Canvas canvas;

    public float time = 3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _inputProvider = FindFirstObjectByType<InputProvider>();
        _inputProvider.PlayerActions.Win.performed += OnWinPerformed;
    }

    private void Win()
    {
        if (won) return;

        won = true;

        _inputProvider.gameObject.SetActive(false);
        canvas.gameObject.SetActive(true);

        Time.timeScale = 0f;
        AudioListener.volume = 0f;

        StartCoroutine(Delay());
    }

    private void OnWinPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Win();
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSecondsRealtime(time);

        SceneManager.LoadScene("Main_Menu");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Win();
        }
    }
}
