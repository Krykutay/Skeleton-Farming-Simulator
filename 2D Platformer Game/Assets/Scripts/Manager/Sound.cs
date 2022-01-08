using System;
using UnityEngine;
using UnityEngine.Audio;

[Serializable]
public class Sound
{
    public SoundManager.SoundTags name;

    public SoundManager.SoundTypes type;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 1f;

    [Range(.1f, 3f)]
    public float pitch = 1f;

    [NonSerialized] public float _defaultMaxVolume;

    public bool isLoop;
    public bool hasCooldown;
    public bool ignoreListenerPause;
    public AudioSource source;
}