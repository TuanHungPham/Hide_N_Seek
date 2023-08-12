using UnityEngine;
using UnityEngine.Serialization;

public class Controller : MonoBehaviour
{
    #region private

    [SerializeField] protected PlayerType _playerType;

    [SerializeField] protected Transform _model;

    [SerializeField] protected InGameState _inGameState;

    [SerializeField] protected InteractingSystem _interactingSystem;

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
        _model = GetComponentInChildren<Transform>();
        _interactingSystem = GetComponent<InteractingSystem>();
    }

    public virtual Transform GetMeshRenderer()
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

    public virtual PlayerType GetPlayerType()
    {
        return _playerType;
    }

    public void SetCaughtState(bool set)
    {
        _inGameState.SetCaughtState(set);
    }

    public void SetSeekerState(bool set)
    {
        _inGameState.SetSeekerState(set);
    }
}