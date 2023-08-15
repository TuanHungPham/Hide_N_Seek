using System.Collections.Generic;
using UnityEngine;

public class GameplaySystem : MonoBehaviour
{
    private static GameplaySystem instance;

    public static GameplaySystem Instance => instance;

    #region private

    [SerializeField] private float _seekerCircleVisonRadius;
    [SerializeField] private float _seekerFrontVisonRadius;

    [Space(20)] [SerializeField] private AllPlayerManager _allPlayerManager;
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

    public List<Transform> GetHiderList()
    {
        return _allPlayerManager.GetHiderList();
    }

    public Vector3 GetNearestSeekerPosition(Transform currentObjCheck)
    {
        return _allPlayerManager.GetNearestSeekerPosition(currentObjCheck);
    }

    public bool IsInHidingTimer()
    {
        return _startingGameSystem.IsInHidingTimer();
    }

    public float GetHidingTime()
    {
        return _startingGameSystem.GetHidingTime();
    }

    public Transform GetMainCharacterReference()
    {
        return _startingGameSystem.GetMainCharacterReference();
    }

    public bool IsSeekerGameplay()
    {
        return _startingGameSystem.IsSeekerGameplay();
    }

    public float GetSeekerCircleVisionRadius()
    {
        return _seekerCircleVisonRadius;
    }

    public float GetSeekerFrontVisionRadius()
    {
        return _seekerFrontVisonRadius;
    }
}