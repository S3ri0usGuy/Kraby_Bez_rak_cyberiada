using UnityEngine;

public class AudioEntry : MonoBehaviour
{
    private AudioSource _source;
    private PitchShifter _shifter;

    public string Key = "audio";

    private void Start()
    {
        _source = GetComponent<AudioSource>();
        _shifter = GetComponent<PitchShifter>();
    }

    public void PlaySound()
    {
        if (_shifter)
        {
            _shifter.PlaySound();
        }
        else
        {
            _source.Play();
        }
    }
}
