using System.Collections;
using UnityEngine;

public class RandomSoundPlayer : MonoBehaviour
{
    public AudioClip[] soundEffects; // Tablica do przechowywania efekt�w d�wi�kowych
    private AudioSource audioSource; // Komponent AudioSource

    private void Start()
    {
        // Dodaj komponent AudioSource do tego obiektu
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false; // Upewnij si�, �e nie odtwarza si� na pocz�tku

        // Rozpocznij coroutine do odtwarzania d�wi�k�w w losowych odst�pach
        StartCoroutine(PlayRandomSounds());
    }

    private IEnumerator PlayRandomSounds()
    {
        while (true)
        {
            // Wybierz losowy czas oczekiwania
            float waitTime = GetRandomWaitTime();
            yield return new WaitForSeconds(waitTime);
            PlayRandomSound();
        }
    }

    private float GetRandomWaitTime()
    {
        // Wybierz losowy czas oczekiwania: 30, 20 lub 15 sekund
        if (Time.time < 30f)
        {
            return 30f; // Pierwsze odtwarzanie po 30 sekundach
        }
        else if (Time.time < 50f)
        {
            return 20f; // Drugie odtwarzanie po 20 sekundach
        }
        else
        {
            return Random.Range(10f, 20f); // Odtwarzaj co 10-20 sekund
        }
    }

    private void PlayRandomSound()
    {
        if (soundEffects.Length == 0)
        {
            Debug.LogWarning("Brak przypisanych efekt�w d�wi�kowych do RandomSoundPlayer.");
            return; // Sprawd�, czy s� jakiekolwiek efekty d�wi�kowe
        }

        // Wybierz losowy efekt d�wi�kowy
        int randomIndex = Random.Range(0, soundEffects.Length);
        audioSource.clip = soundEffects[randomIndex];
        audioSource.Play();

        Debug.Log($"Odtwarzanie d�wi�ku: {soundEffects[randomIndex].name}"); // Loguj nazw� odtwarzanego d�wi�ku
    }
}