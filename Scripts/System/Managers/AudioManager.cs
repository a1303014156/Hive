using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioSource sfxPlayer;

    private const float MinPitch = 0.9f;
    private const float MaxPitch = 1.1f;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    public void PlaySfx(AudioData data)
    {
        sfxPlayer.PlayOneShot(data.clip, data.volume);
    }

    public void PlayRandomSfx(AudioData data)
    {
        sfxPlayer.pitch = Random.Range(MinPitch, MaxPitch);
        PlaySfx(data);
    }

    public void PlayRandomSfx(AudioData[] data)
    {
        sfxPlayer.pitch = Random.Range(MinPitch, MaxPitch);
        PlaySfx(data[Random.Range(0, data.Length)]);
    }
}

[System.Serializable]
public class AudioData
{
    public AudioClip clip;
    public float volume;
}
