using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameplaySystem : MonoBehaviour
{
    private static GameplaySystem instance;

    public static GameplaySystem Instance => instance;

    #region private

    [SerializeField] private AllPlayerManager _allPlayerManager;
    [SerializeField] private StartingGameSystem _startingGameSystem;

    #endregion

    private void Awake()
    {
        instance = this;
        LoadComponents();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        _allPlayerManager = GetComponentInChildren<AllPlayerManager>();
        _startingGameSystem = GetComponentInChildren<StartingGameSystem>();
    }

    public AllPlayerManager GetAllPlayerManager()
    {
        return _allPlayerManager;
    }

    public StartingGameSystem GetStartingGameSystem()
    {
        return _startingGameSystem;
    }

    public List<Transform> GetAllPlayerList()
    {
        return _allPlayerManager.GetAllPlayerList();
    }

    public Vector3 GetNearestSeekerPosition(Transform currentObjCheck)
    {
        return _allPlayerManager.GetNearestSeekerPosition(currentObjCheck);
    }

    public bool IsInHidingTimer()
    {
        return _startingGameSystem.IsInHidingTimer();
    }

    public Transform GetMainCharacterReference()
    {
        return _startingGameSystem.GetMainCharacterReference();
    }
}