using UnityEngine;
using UnityEngine.Serialization;

public class Controller : MonoBehaviour
{
    #region private

    [SerializeField] protected InGameState inGameState;
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
        inGameState = GetComponentInChildren<InGameState>();
        _interactingSystem = GetComponent<InteractingSystem>();
    }

    public virtual InGameState GetInGameState()
    {
        return inGameState;
    }

    public virtual InteractingSystem GetInteractingSystem()
    {
        return _interactingSystem;
    }

    public void SetCaughtState(bool set)
    {
        inGameState.SetCaughtState(set);
    }
}