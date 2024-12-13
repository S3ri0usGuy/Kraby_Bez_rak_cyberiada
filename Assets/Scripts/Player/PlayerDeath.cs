using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Damagable))]
public class PlayerDeath : MonoBehaviour
{
    private Damagable _damagable;

    private Animator _animator;

    [SerializeField]
    private float deathTimer = 3f;

    private void Awake()
    {
        _damagable = GetComponent<Damagable>();
        _animator = GetComponent<Animator>();
        _damagable.Died += OnDied;
    }

    private void OnDied(Damagable damagable)
    {
        _animator.SetTrigger("death");
        StartCoroutine(ReloadScene());
    }

    private IEnumerator ReloadScene()
    {
        yield return new WaitForSecondsRealtime(deathTimer);

        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
