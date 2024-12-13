using UnityEngine;

public class FootstepController : MonoBehaviour
{
    public AudioClip[] footstepSounds; // Tablica dźwięków kroków
    public float minTimeBetweenFootsteps = 0.3f; // Minimalny czas między dźwiękami kroków
    public float maxTimeBetweenFootsteps = 0.6f; // Maksymalny czas między dźwiękami kroków

    private AudioSource audioSource; // Referencja do komponentu AudioSource
    private bool isWalking = false; // Flaga do śledzenia, czy postać chodzi
    private float timeSinceLastFootstep; // Czas od ostatniego dźwięku kroku

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>(); // Pobierz komponent AudioSource
    }

    private void Update()
    {
        if (isWalking)
        {
            // Sprawdź, czy minął wystarczający czas, aby odtworzyć następny dźwięk kroku
            if (Time.time - timeSinceLastFootstep >= Random.Range(minTimeBetweenFootsteps, maxTimeBetweenFootsteps))
            {
                // Losuj dźwięk kroku z tablicy
                AudioClip footstepSound = footstepSounds[Random.Range(0, footstepSounds.Length)];
                audioSource.PlayOneShot(footstepSound); // Odtwórz dźwięk kroku
                timeSinceLastFootstep = Time.time; // Zaktualizuj czas od ostatniego dźwięku kroku
            }
        }
    }

    // Wywołaj tę metodę, gdy postać zaczyna chodzić
    public void StartWalking()
    {
        isWalking = true; // Ustaw flagę na true
    }

    // Wywołaj tę metodę, gdy postać przestaje chodzić
    public void StopWalking()
    {
        isWalking = false; // Ustaw flagę na false
    }
}