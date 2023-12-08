using System;
using System.Collections.Generic;
using UnityEngine;

public enum eSoundType
{
    WIN_SOUND,
    LOSE_SOUND,
    FOOT_STEP,
}

public class SoundManager : TemporaryMonoSingleton<SoundManager>
{
    [SerializeField] private AudioListener _mainAudio;
    [SerializeField] private AudioSource _audioSource;
    private Dictionary<eSoundType, AudioClip> _audioClipDict;

    protected override void Awake()
    {
        base.Awake();
        LoadComponents();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        _mainAudio = GetComponent<AudioListener>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        InitializeSoundDict();
        PlaySound(eSoundType.WIN_SOUND);
    }

    private void InitializeSoundDict()
    {
        _audioClipDict = new Dictionary<eSoundType, AudioClip>();

        AddSoundClipToSoundDict(eSoundType.WIN_SOUND, "Sound/applause-crowd-cheering-sfx");
        AddSoundClipToSoundDict(eSoundType.LOSE_SOUND, "Sound/119119758-win-fanfare-2");
        AddSoundClipToSoundDict(eSoundType.FOOT_STEP, "Sound/footsteps");
    }

    private void AddSoundClipToSoundDict(eSoundType soundType, string path)
    {
        AudioClip audioClip = Resources.Load<AudioClip>(path);
        _audioClipDict.Add(soundType, audioClip);
    }

    private void PlaySound(eSoundType soundType)
    {
        _audioSource.clip = _audioClipDict[soundType];
        _audioSource.Play();
    }
}