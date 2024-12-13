using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PitchShifter : MonoBehaviour
{
    private AudioSource _audio;

    [SerializeField]
    private float minPitch = 0.95f;
    [SerializeField]
    private float maxPitch = 1.05f;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _audio.pitch = Random.Range(minPitch, maxPitch);
    }

    private void UpdatePitch()
    {
        _audio.pitch = Random.Range(minPitch, maxPitch);
    }

    public void PlaySound()
    {
        UpdatePitch();
        _audio.Play();
    }
}
