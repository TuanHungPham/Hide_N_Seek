using MarchingBytes;
using UnityEngine;

public class EffectVFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    private void OnEnable()
    {
        _particleSystem.Play();

        var timeDelay = _particleSystem.main.duration;
        Invoke(nameof(SelfDestroy), timeDelay);
    }

    private void SelfDestroy()
    {
        EasyObjectPool.instance.ReturnObjectToPool(gameObject);
    }
}