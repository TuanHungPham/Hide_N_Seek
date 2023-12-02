using UnityEngine;
using UnityEngine.Serialization;

public class Controller : MonoBehaviour
{
    #region private

    [FormerlySerializedAs("_playerType")] [SerializeField]
    protected ePlayerType ePlayerType;

    [SerializeField] protected Model _model;

    [SerializeField] protected InGameState _inGameState;

    [SerializeField] protected InteractingSystem _interactingSystem;
    [SerializeField] protected SeekerVisionInteractingSystem _seekerVision;
    [SerializeField] protected AnimationHandler _animationHandler;
    [SerializeField] protected PetHolderHandler _petHolderHandler;

    private GameplaySystem GameplaySystem => GameplaySystem.Instance;

    #endregion

    protected virtual void Awake()
    {
        LoadComponents();
    }

    protected virtual void Reset()
    {
        LoadComponents();
    }

    protected virtual void LoadComponents()
    {
        _inGameState = GetComponentInChildren<InGameState>();
        _model = GetComponentInChildren<Model>();
        _interactingSystem = GetComponentInChildren<InteractingSystem>();
        _petHolderHandler = GetComponentInChildren<PetHolderHandler>();
    }

    public virtual InGameState GetInGameState()
    {
        return _inGameState;
    }

    public virtual ePlayerType GetPlayerType()
    {
        return ePlayerType;
    }

    public void SetModelMaterial(Material material)
    {
        _model.SetModelMaterial(material);
    }

    public void SetTriggeredState(bool set)
    {
        _inGameState.SetTriggerState(set);
    }

    public void SetCaughtState(bool set)
    {
        if (set)
        {
            GameplaySystem.AddToHiderCaughtList(transform);
        }
        else
        {
            GameplaySystem.RemoveFromHiderCaughtList(transform);
        }

        _inGameState.SetCaughtState(set);
    }

    public void SetSeekerState(bool set)
    {
        _inGameState.SetSeekerState(set);
    }

    public void SetDetectedState(bool set)
    {
        _inGameState.SetDetectedState(set);
    }

    public void SetIdleAnimationState()
    {
        _animationHandler.SetIdleAnimation();
    }

    public void SetMovingAnimation(float value)
    {
        _animationHandler.SetNormalizedVelocity(value);
    }

    public void ChangePet(GameObject pet)
    {
        _petHolderHandler.ChangePet(pet);
    }

    public void SetLightRange(eVisionType visionType, float value)
    {
        _seekerVision.SetLightRange(visionType, value);
    }

    public void SetLightHeight(eVisionType visionType, float value)
    {
        _seekerVision.SetLightHeight(visionType, value);
    }

    public void SetLightIntensity(eVisionType visionType, float value)
    {
        _seekerVision.SetLightIntensity(visionType, value);
    }
}