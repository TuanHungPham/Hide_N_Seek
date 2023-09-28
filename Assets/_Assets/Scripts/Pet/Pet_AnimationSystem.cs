using UnityEngine;

public class Pet_AnimationSystem : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private const string NORMALIZE_VELOCITY_PARAM = "NormalizedVelocity";

    private void Awake()
    {
        LoadComponents();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetMovingAnimation()
    {
        _animator.SetFloat(NORMALIZE_VELOCITY_PARAM, 1);
    }

    public void SetIdleAnimation()
    {
        _animator.SetFloat(NORMALIZE_VELOCITY_PARAM, 0, .3f, Time.deltaTime);
    }
}