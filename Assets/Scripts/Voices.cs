using System.Collections;
using UnityEngine;

public class RandomSoundPlayer : MonoBehaviour
{
    public AudioClip[] soundEffects; // Tablica do przechowywania efektów dŸwiêkowych
    private AudioSource audioSource; // Komponent AudioSource

    private void Start()
    {
        // Dodaj komponent AudioSource do tego obiektu
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false; // Upewnij siê, ¿e nie odtwarza siê na pocz¹tku

        // Rozpocznij coroutine do odtwarzania dŸwiêków w losowych odstêpach
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
            Debug.LogWarning("Brak przypisanych efektów dŸwiêkowych do RandomSoundPlayer.");
            return; // SprawdŸ, czy s¹ jakiekolwiek efekty dŸwiêkowe
        }

        // Wybierz losowy efekt dŸwiêkowy
        int randomIndex = Random.Range(0, soundEffects.Length);
        audioSource.clip = soundEffects[randomIndex];
        audioSource.Play();

        Debug.Log($"Odtwarzanie dŸwiêku: {soundEffects[randomIndex].name}"); // Loguj nazwê odtwarzanego dŸwiêku
    }
}