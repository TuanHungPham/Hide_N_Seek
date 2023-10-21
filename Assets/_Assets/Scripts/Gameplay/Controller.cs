using UnityEngine;
using UnityEngine.Serialization;

public class Controller : MonoBehaviour
{
    #region private

    [FormerlySerializedAs("_playerType")] [SerializeField] protected ePlayerType ePlayerType;

    [SerializeField] protected Model _model;

    [SerializeField] protected InGameState _inGameState;

    [SerializeField] protected InteractingSystem _interactingSystem;
    [SerializeField] protected SeekerVisionInteractingSystem _seekerVision;
    [SerializeField] protected AnimationHandler _animationHandler;
    [SerializeField] protected PetHolderHandler _petHolderHandler;

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
        _seekerVision = GetComponentInChildren<SeekerVisionInteractingSystem>();
        _interactingSystem = GetComponentInChildren<InteractingSystem>();
        _petHolderHandler = GetComponentInChildren<PetHolderHandler>();
    }

    public virtual Model GetModel()
    {
        return _model;
    }

    public virtual InGameState GetInGameState()
    {
        return _inGameState;
    }

    public virtual InteractingSystem GetInteractingSystem()
    {
        return _interactingSystem;
    }

    public virtual ePlayerType GetPlayerType()
    {
        return ePlayerType;
    }

    public SeekerVisionInteractingSystem GetSeekerVision()
    {
        return _seekerVision;
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

    public void SetAnimationState(int animationID)
    {
        _animationHandler.SetAnimation(animationID);
    }

    public void SetIdleAnimationState()
    {
        _animationHandler.SetIdleAnimation();
    }

    public void SetMovingAnimation(float value)
    {
        _animationHandler.SetNormalizedVelocity(value);
    }

    public void SetPetToHolder(GameObject pet)
    {
        _petHolderHandler.SetPetToHolder(pet);
    }

    public Transform GetPetHolder()
    {
        return _petHolderHandler.GetPetHolder();
    }

    public Vector3 GetPetHolderPosition()
    {
        return _petHolderHandler.GetPetHolderPosition();
    }

    public void ChangePet(GameObject pet)
    {
        _petHolderHandler.ChangePet(pet);
    }
}