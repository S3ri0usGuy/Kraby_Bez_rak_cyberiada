using System.Collections;
using UnityEngine;

public class RandomSoundPlayer : MonoBehaviour
{
    public AudioClip[] soundEffects; // Array to hold sound effects
    private AudioSource audioSource; // AudioSource component

    private void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false; // Ensure it doesn't play on awake

        // Start the coroutine to play sounds at specified intervals
        StartCoroutine(PlayRandomSounds());
    }

    private IEnumerator PlayRandomSounds()
    {
        // Wait for 30 seconds
        yield return new WaitForSeconds(30f);
        PlayRandomSound();

        // Wait for 20 seconds
        yield return new WaitForSeconds(20f);
        PlayRandomSound();

        // Now play every 15 seconds
        while (true)
        {
            yield return new WaitForSeconds(15f);
            PlayRandomSound();
        }
    }

    private void PlayRandomSound()
    {
        if (soundEffects.Length == 0)
        {
            Debug.LogWarning("No sound effects assigned to the RandomSoundPlayer.");
            return; // Check if there are any sound effects
        }

        // Select a random sound effect
        int randomIndex = Random.Range(0, soundEffects.Length);
        audioSource.clip = soundEffects[randomIndex];
        audioSource.Play();

        Debug.Log($"Playing sound: {soundEffects[randomIndex].name}"); // Log the name of the sound being played
    }
}