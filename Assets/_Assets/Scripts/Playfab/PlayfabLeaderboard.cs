using System.Collections.Generic;
using fbg;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class PlayfabLeaderboard : MonoBehaviour
{
    [SerializeField] List<StatisticUpdate> statisticUpdateList = new List<StatisticUpdate>();
    private PlayfabManager PlayfabManager => PlayfabManager.Instance;
    private InGameManager InGameManager => InGameManager.Instance;

    public void UpdatePlayerStatistic()
    {
        GetStatisticUpdateList();

        UpdatePlayerStatisticsRequest request = new UpdatePlayerStatisticsRequest()
        {
            Statistics = statisticUpdateList
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

        statisticUpdateList.Clear();

        StatisticUpdate winningStatistic = new StatisticUpdate()
        {
            StatisticName = "Win",
            Value = winningTime
        };

        StatisticUpdate catchingStatistic = new StatisticUpdate()
        {
            StatisticName = "Catch",
            Value = catchingTime
        };

        StatisticUpdate rescuingStatistic = new StatisticUpdate()
        {
            StatisticName = "Rescue",
            Value = rescuingTime
        };

        statisticUpdateList.Add(winningStatistic);
        statisticUpdateList.Add(catchingStatistic);
        statisticUpdateList.Add(rescuingStatistic);
    }
}