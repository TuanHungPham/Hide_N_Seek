using System;
using System.Collections.Generic;
using PlayFab.ClientModels;
using UnityEngine;

public enum eLeaderboardType
{
    WIN,
    CATCH,
    RESCUE,
}

public class LeaderboardPanel : MonoBehaviour
{
    [SerializeField] private eLeaderboardType _leaderboardType;

    [SerializeField] private GameObject _firstRank;
    [SerializeField] private GameObject _secondRank;
    [SerializeField] private GameObject _thirdRank;
    [SerializeField] private GameObject _normalRank;

    [SerializeField] private List<RankPanel> _rankPanelList = new List<RankPanel>();

    private PlayfabManager PlayfabManager => PlayfabManager.Instance;

    private void Awake()
    {
        InitializeRankPanel();
    }

    private void OnEnable()
    {
        RefreshLeaderboardPanel();
    }

    private void InitializeRankPanel()
    {
        for (int i = 1; i <= PlayfabManager.GetNumberOfUserOnLeaderboard(); i++)
        {
            CreateRankPanel(i);
        }
    }

    private void CreateRankPanel(int rank)
    {
        GameObject rankPanelObj;

        switch (rank)
        {
            case 1:
                rankPanelObj = Instantiate(_firstRank, transform, true);
                break;
            case 2:
                rankPanelObj = Instantiate(_secondRank, transform, true);
                break;
            case 3:
                rankPanelObj = Instantiate(_thirdRank, transform, true);
                break;
            default:
                rankPanelObj = Instantiate(_normalRank, transform, true);
                break;
        }

        RankPanel rankPanel = rankPanelObj.GetComponent<RankPanel>();
        _rankPanelList.Add(rankPanel);

        SetupRankPanelUI(rankPanel, rank);
    }

    private void RefreshLeaderboardPanel()
    {
        List<PlayerLeaderboardEntry> leaderboard = PlayfabManager.GetLearderboard(_leaderboardType);

        for (int i = 0; i < leaderboard.Count; i++)
        {
            string name = leaderboard[i].DisplayName;
            int point = leaderboard[i].StatValue;
            _rankPanelList[i].SetUI(name, point);
        }
    }

    private void SetupRankPanelUI(RankPanel rankPanel, int rank)
    {
        rankPanel.SetUI("", 0, rank.ToString());
    }

    public void SetLeaderboardType(eLeaderboardType type)
    {
        _leaderboardType = type;
        RefreshLeaderboardPanel();
    }
}