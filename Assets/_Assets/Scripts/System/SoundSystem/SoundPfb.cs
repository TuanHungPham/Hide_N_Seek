using System;
using MarchingBytes;
using UnityEngine;

public class SoundPfb : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _delayTime;

    private void Awake()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void SetSound(AudioClip audioClip)
    {
        _audioSource.clip = audioClip;
        _audioSource.Play();
        SelfDestroyAfterSoundEnd();
    }

    private void SelfDestroyAfterSoundEnd()
    {
        if (_audioSource.clip == null) return;

        float timeDelay = _audioSource.clip.length + _delayTime;
        Invoke(nameof(Destroy), timeDelay);
    }

    private void Destroy()
    {
        EasyObjectPool.instance.ReturnObjectToPool(gameObject);
    }
}