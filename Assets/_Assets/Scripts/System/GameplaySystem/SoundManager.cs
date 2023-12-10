using System;
using System.Collections.Generic;
using MarchingBytes;
using UnityEngine;

public enum eSoundType
{
    WIN_SOUND,
    LOSE_SOUND,
    FOOT_STEP,
    COIN_PICKUP,
    TIME_TICK,
}

public class SoundManager : TemporaryMonoSingleton<SoundManager>
{
    [SerializeField] private AudioListener _mainAudio;
    [SerializeField] private AudioSource _audioSource;
    private Dictionary<eSoundType, AudioClip> _audioClipDict;
    private EasyObjectPool EasyObjectPool => EasyObjectPool.instance;
    private IngameSetting IngameSetting => IngameSetting.Instance;

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
    }

    private void InitializeSoundDict()
    {
        _audioClipDict = new Dictionary<eSoundType, AudioClip>();

        AddSoundClipToSoundDict(eSoundType.WIN_SOUND, "Sound/applause-crowd-cheering-sfx");
        AddSoundClipToSoundDict(eSoundType.LOSE_SOUND, "Sound/119119758-win-fanfare-2");
        AddSoundClipToSoundDict(eSoundType.FOOT_STEP, "Sound/concrete-footsteps-1-6265");
        AddSoundClipToSoundDict(eSoundType.COIN_PICKUP, "Sound/collect-golden-coins");
        AddSoundClipToSoundDict(eSoundType.TIME_TICK, "Sound/tick");
    }

    private void AddSoundClipToSoundDict(eSoundType soundType, string path)
    {
        AudioClip audioClip = Resources.Load<AudioClip>(path);
        _audioClipDict.Add(soundType, audioClip);
    }

    public void PlaySound(eSoundType soundType)
    {
        if (!IngameSetting.GetSettingState(eSetting.SFX_SOUND)) return;

        _audioSource.clip = _audioClipDict[soundType];
        _audioSource.Play();
    }

    public void PlaySFX(eSoundType soundType, Vector3 position)
    {
        if (!IngameSetting.GetSettingState(eSetting.SFX_SOUND)) return;

        GameObject soundPfb = EasyObjectPool.GetObjectFromPool(PoolName.SOUND_POOL, position, Quaternion.identity);

        SoundPfb soundPfbController = soundPfb.GetComponent<SoundPfb>();
        soundPfbController.SetSound(_audioClipDict[soundType]);
    }
}