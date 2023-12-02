using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private const string ANIMATION_ID = "AnimationID";
    private const string IS_PLAYER_PARAM = "IsPlayer";
    private const string NORMALIZED_VELOCITY_VALUE = "NormalizedVelocity";
    private const int ANIMATION_TRANSITION_STATE = 0;
    [SerializeField] private float _currentAnimValue;
    [SerializeField] private Animator _animator;

    public void SetAnimation(int animationID)
    {
        _animator.SetInteger(ANIMATION_ID, ANIMATION_TRANSITION_STATE);
        _animator.SetInteger(ANIMATION_ID, animationID);
    }

    public void SetIdleAnimation()
    {
        _animator.SetFloat(IS_PLAYER_PARAM, 1);

        _animator.SetFloat(NORMALIZED_VELOCITY_VALUE, 0, 0.05f, Time.deltaTime);
        _animator.SetInteger(ANIMATION_ID, ANIMATION_TRANSITION_STATE);
    }

    public void SetNormalizedVelocity(float value)
    {
        _animator.SetFloat(NORMALIZED_VELOCITY_VALUE, value);
    }
}