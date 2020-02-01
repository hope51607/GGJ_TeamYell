using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Music {
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume;
}

[System.Serializable]
public class SE
{
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume;
}