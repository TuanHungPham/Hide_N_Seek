using System;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using TigerForge;
using UnityEngine;

public class PlayfabLeaderboard : MonoBehaviour
{
    private const string WIN_STATISTIC = "Win";
    private const string CATCH_STATISTIC = "Catch";
    private const string RESCUE_STATISTIC = "Rescue";
    [SerializeField] private int _maxNumberOfUserOnLeaderboard;
    [SerializeField] private List<StatisticUpdate> _statisticUpdateList = new List<StatisticUpdate>();
    private Dictionary<eLeaderboardType, List<PlayerLeaderboardEntry>> _leaderboardDictionary = new Dictionary<eLeaderboardType, List<PlayerLeaderboardEntry>>();

    private InGameManager InGameManager => InGameManager.Instance;

    private void Start()
    {
        ListenEvent();
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.LOGIN_SUCCESS, GetAllLeaderboardFromServer);
    }

    public void UpdatePlayerStatistic()
    {
        GetStatisticUpdateList();

        UpdatePlayerStatisticsRequest request = new UpdatePlayerStatisticsRequest()
        {
            Statistics = _statisticUpdateList
        };

        PlayFabClientAPI.UpdatePlayerStatistics(request, OnUpdateStatisticCallback, OnErrorCallback);
    }

    private void OnErrorCallback(PlayFabError error)
    {
        Debug.LogError($"(PLAYFAB) Playfab Error: {error.ErrorMessage}");
    }

    private void OnUpdateStatisticCallback(UpdatePlayerStatisticsResult result)
    {
        Debug.Log($"(PLAYFAB) Update Statistic result: {result}");
    }

    private void GetStatisticUpdateList()
    {
        int winningTime = Mathf.CeilToInt(InGameManager.GetAchievementPoint(eAchievementType.WINNING_TIME));
        int catchingTime = Mathf.CeilToInt(InGameManager.GetAchievementPoint(eAchievementType.CATCHING_TIME));
        int rescuingTime = Mathf.CeilToInt(InGameManager.GetAchievementPoint(eAchievementType.RESCUING_TIME));

        _statisticUpdateList.Clear();

        StatisticUpdate winningStatistic = new StatisticUpdate()
        {
            StatisticName = WIN_STATISTIC,
            Value = winningTime
        };

        StatisticUpdate catchingStatistic = new StatisticUpdate()
        {
            StatisticName = CATCH_STATISTIC,
            Value = catchingTime
        };

        StatisticUpdate rescuingStatistic = new StatisticUpdate()
        {
            StatisticName = RESCUE_STATISTIC,
            Value = rescuingTime
        };

        _statisticUpdateList.Add(winningStatistic);
        _statisticUpdateList.Add(catchingStatistic);
        _statisticUpdateList.Add(rescuingStatistic);
    }

    public void GetAllLeaderboardFromServer()
    {
        GetLeaderboardFromServer(WIN_STATISTIC);
        GetLeaderboardFromServer(CATCH_STATISTIC);
        GetLeaderboardFromServer(RESCUE_STATISTIC);
    }

    private void GetLeaderboardFromServer(string leaderboardName)
    {
        GetLeaderboardRequest request = new GetLeaderboardRequest()
        {
            StatisticName = leaderboardName,
            StartPosition = 0,
            MaxResultsCount = _maxNumberOfUserOnLeaderboard
        };

        PlayFabClientAPI.GetLeaderboard(request,
            result => OnGettingLeaderboardResult(result, leaderboardName),
            OnErrorCallback);
    }

    private void OnGettingLeaderboardResult(GetLeaderboardResult result, string leaderboardName)
    {
        Debug.Log($"(PLAYFAB) Getting {leaderboardName} Leaderboard result: {result.Leaderboard}");
        switch (leaderboardName)
        {
            case WIN_STATISTIC:
                AddLeaderboard(eLeaderboardType.WIN, result);
                break;
            case CATCH_STATISTIC:
                AddLeaderboard(eLeaderboardType.CATCH, result);
                break;
            case RESCUE_STATISTIC:
                AddLeaderboard(eLeaderboardType.RESCUE, result);
                break;
        }
    }

    private void AddLeaderboard(eLeaderboardType type, GetLeaderboardResult result)
    {
        if (_leaderboardDictionary.ContainsKey(type))
        {
            _leaderboardDictionary[type] = result.Leaderboard;
            return;
        }

        _leaderboardDictionary.Add(type, result.Leaderboard);
    }

    public int GetNumberOfUserOnLeaderboard()
    {
        return _maxNumberOfUserOnLeaderboard;
    }

    public List<PlayerLeaderboardEntry> GetLeaderboard(eLeaderboardType type)
    {
        return _leaderboardDictionary[type];
    }
}