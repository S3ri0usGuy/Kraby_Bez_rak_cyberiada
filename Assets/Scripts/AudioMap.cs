using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioMap : MonoBehaviour
{
    private Dictionary<string, AudioEntry> _dict;

    private void Start()
    {
        _dict = new();
        AudioEntry[] shifters = GetComponentsInChildren<AudioEntry>(true);

        _dict = shifters.ToDictionary(x => x.Key, x => x);
    }

    public void PlaySound(string key)
    {
        _dict[key].PlaySound();
    }
}
