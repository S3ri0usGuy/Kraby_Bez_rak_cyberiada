//using UnityEngine;
//using System;

//public class FootstepController : MonoBehaviour
//{
//public AudioClip[] footstepSounds;
//public float minTimeBeetwenFootSteps = 0,3f;
//public float maxTimeBeetweeFootSteps = 0,6f;

//private AudioSource audioSource;
//private bool isWalking = false;
//private float timeSinceLastFootStep;

//private void Awake()
//{
//    audioSource = GetComponent<AudioSource>();
//}

//private void Update()
//{
//    if (isWalking)
//    {
//        if (Time.time - timeSinceLastFootStep >=  Random.Range(minTimeBetweenFootsteps, maxTimeBetweenFootsteps))
//        {
//            AudioClip footstepSounds [Random.Range(0, footstepSounds.Length)];
//            audioSource.PlayOneShot(footstepSound);
//              timeSinceLastFootstep = Time.time; // Zaktualizuj czas od ostatniego dźwięku kroku
//        }
//    }
//}

//// Wywołaj tę metodę, gdy postać zaczyna chodzić
//    public void StartWalking()
//    {
//        isWalking = true; // Ustaw flagę na true
//    }

//    // Wywołaj tę metodę, gdy postać przestaje chodzić
//    public void StopWalking()
//    {
//        isWalking = false; // Ustaw flagę na false
//    }
//}
