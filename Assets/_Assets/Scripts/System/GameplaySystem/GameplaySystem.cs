using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameplaySystem : TemporaryMonoSingleton<GameplaySystem>
{
    #region private

    [SerializeField] private float _seekerCircleVisonRadius;
    [SerializeField] private float _seekerFrontVisonRadius;

    [Space(20)] [SerializeField] private AllPlayerManager _allPlayerManager;
    [SerializeField] private InitializingGameplaySystem initializingGameplaySystem;
    [SerializeField] private GameplayTimeSystem _gameplayTimeSystem;

    #endregion

    protected override void Awake()
    {
        base.Awake();
        LoadComponents();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        _allPlayerManager = GetComponentInChildren<AllPlayerManager>();
        initializingGameplaySystem = GetComponentInChildren<InitializingGameplaySystem>();
        _gameplayTimeSystem = GetComponentInChildren<GameplayTimeSystem>();
    }

    public List<Transform> GetAllPlayerList()
    {
        return _allPlayerManager.GetAllPlayerList();
    }

    public List<Transform> GetHiderList()
    {
        return _allPlayerManager.GetHiderList();
    }

    public List<Transform> GetHiderCaughtList()
    {
        return _allPlayerManager.GetCaughtList();
    }

    public Vector3 GetNearestSeekerPosition(Transform currentObjCheck)
    {
        return _allPlayerManager.GetNearestSeekerPosition(currentObjCheck);
    }

    public bool IsInHidingTimer()
    {
        return _gameplayTimeSystem.IsInHidingTimer();
    }

    public bool IsGameStarting()
    {
        return initializingGameplaySystem.IsGameStarting();
    }

    public bool IsTimeUp()
    {
        return _gameplayTimeSystem.IsTimeUp();
    }

    public float GetGameplayTimer()
    {
        return _gameplayTimeSystem.GetGameplayTimer();
    }

    public float GetGameplayTime()
    {
        return _gameplayTimeSystem.GetGameplayTime();
    }

    public float GetHidingTime()
    {
        return _gameplayTimeSystem.GetHidingTime();
    }

    public float GetHidingTimer()
    {
        return _gameplayTimeSystem.GetHidingTimer();
    }

    public int GetNumberOfHider()
    {
        return _allPlayerManager.GetNumberOfHider();
    }

    public int GetNumberOfCaughtHider()
    {
        return _allPlayerManager.GetNumberOfCaughtHider();
    }

    public int GetRequirementNumberOfCaughtHider()
    {
        return _allPlayerManager.GetRequirementNumberOfCaughtHider();
    }

    public bool IsSeekerGameplay()
    {
        return initializingGameplaySystem.IsSeekerGameplay();
    }

    public float GetSeekerCircleVisionRadius()
    {
        return _seekerCircleVisonRadius;
    }

    public List<Transform> GetSeekerList()
    {
        return _allPlayerManager.GetSeekerList();
    }

    public void SetGameplayType(bool isSeekerGameplay)
    {
        initializingGameplaySystem.SetGameplayType(isSeekerGameplay);
    }

    public void SetNumberOfSeeker(int number)
    {
        initializingGameplaySystem.SetNumberOfSeeker(number);
    }

    public void SetGameplayTimer(float hidingTimer)
    {
        _gameplayTimeSystem.SetGameplayTimer(hidingTimer);
    }

    public void AddToHiderCaughtList(Transform obj)
    {
        _allPlayerManager.AddToCaughtList(obj);
    }

    public void RemoveFromHiderCaughtList(Transform obj)
    {
        _allPlayerManager.RemoveFromCaughtList(obj);
    }
}