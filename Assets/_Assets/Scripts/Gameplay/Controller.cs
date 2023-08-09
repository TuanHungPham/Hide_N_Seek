using UnityEngine;

public class Controller : MonoBehaviour
{
    #region private

    [SerializeField] protected InGameState inGameState;

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
    }
}