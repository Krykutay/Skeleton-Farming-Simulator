using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public Sound[] sounds;
    private static Dictionary<SoundTags, float> soundTimerDictionary;

    public enum SoundTags
    {
        GrassMove1,
        GrassMove2,
        GrassMove3,
        PlayerMelee1,
        PlayerMelee2,
        PlayerMelee3,
        PlayerDash,
        PlayerDashPre,
        PlayerHurt1,
        PlayerHurt2,
        PlayerJump,
        PlayerLand,
        PlayerParry,
        PlayerRun,
        PlayerWallSlide,
        SkeletonAttack1,
        SkeletonAttack2,
        SkeletonAttack3,
        SkeletonBow,
        SkeletonDetection1,
        SkeletonDetection2,
        SkeletonDie,
        SkeletonDodge,
        SkeletonHurt,
        SkeletonRespawn,
        SkeletonSpell,
        SkeletonTeleport,
        PlayerLedgeClimb,
        NpcTalk1_1,
        NpcTalk1_2,
        NpcTalk1_3,
        NpcTalk1_4,
        NpcTalk2_1,
        NpcTalk3_1,
        NpcTalk3_2,
        NpcTalk4_1,
        NpcTalk5_1,
        NpcTalk5_2,
        NpcTalk5_3,
        NpcTalk6_1,
        NpcTalk6_2,
        NpcTalk6_3,
        NpcTalk6_4,
        NpcTalk7_1,
        NpcTalk7_2,
        ButtonClick,
        ScoreCalculation,
    }

    public enum SoundTypes
    {
        Effect,
        Music,
        Voice,
    }

    void Awake()
    {
        Instance = this;

        soundTimerDictionary = new Dictionary<SoundTags, float>();

        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound._defaultMaxVolume = sound.volume;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.isLoop;
            sound.source.playOnAwake = false;

            if (sound.hasCooldown)
            {
                soundTimerDictionary[sound.name] = 0.15f;
            }
        }
    }

    void Start()
    {
        //Play(SoundManager.SoundTags.Ambient);
    }

    public void Play(SoundTags name)
    {
        Sound sound = Array.Find(sounds, s => s.name == name);

        if (sound == null)
        {
            Debug.LogError("Sound " + name + " Not Found!");
            return;
        }

        if (!CanPlaySound(sound)) return;

        sound.source.Play();
    }

    public void Stop(SoundTags name)
    {
        Sound sound = Array.Find(sounds, s => s.name == name);

        if (sound == null)
        {
            Debug.LogError("Sound " + name + " Not Found!");
            return;
        }

        sound.source.Stop();
    }

    private static bool CanPlaySound(Sound sound)
    {
        if (soundTimerDictionary.ContainsKey(sound.name))
        {
            float lastTimePlayed = soundTimerDictionary[sound.name];

            if (lastTimePlayed + (sound.clip.length) / 8 < Time.unscaledTime)
            {
                soundTimerDictionary[sound.name] = Time.unscaledTime;
                return true;
            }
            return false;
        }
        return true;
    }

}