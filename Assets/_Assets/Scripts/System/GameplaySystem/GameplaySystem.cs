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
    [SerializeField] private GameplayTimeSystem _gameplayTimeSystem;

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
        _gameplayTimeSystem = GetComponentInChildren<GameplayTimeSystem>();
    }

    public AllPlayerManager GetAllPlayerManager()
    {
        return _allPlayerManager;
    }

    public StartingGameSystem GetStartingGameSystem()
    {
        return _startingGameSystem;
    }

    public GameplayTimeSystem GetGameplayTimeSystem()
    {
        return _gameplayTimeSystem;
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
        return _gameplayTimeSystem.IsInHidingTimer();
    }

    public bool IsGameStarting()
    {
        return _startingGameSystem.IsGameStarting();
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

    public List<Transform> GetSeekerList()
    {
        return _allPlayerManager.GetSeekerList();
    }

    public void SetGameplayType(bool isSeekerGameplay)
    {
        _startingGameSystem.SetGameplayType(isSeekerGameplay);
    }

    public void SetNumberOfSeeker(int number)
    {
        _startingGameSystem.SetNumberOfSeeker(number);
    }

    public void SetGameplayTimer(float hidingTimer)
    {
        _gameplayTimeSystem.SetGameplayTimer(hidingTimer);
    }
}