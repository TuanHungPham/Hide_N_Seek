using System.Collections.Generic;
using PlayFab.ClientModels;
using UnityEngine;

public class PlayfabManager : MonoBehaviour
{
    private static PlayfabManager instance;
    public static PlayfabManager Instance => instance;

    [SerializeField] private PlayfabAuthentication _playFabAuthentication;
    [SerializeField] private PlayfabLeaderboard _playfabLeaderboard;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        instance = this;
        LoadComponents();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        _playFabAuthentication = GetComponentInChildren<PlayfabAuthentication>();
        _playfabLeaderboard = GetComponentInChildren<PlayfabLeaderboard>();
    }

    public void LoginWithEmail(string email, string password)
    {
        _playFabAuthentication.LoginWithEmail(email, password);
    }

    public void SignUpWithEmail(string email, string password, string displayName)
    {
        _playFabAuthentication.SignUpWithEmail(email, password, displayName);
    }

    public void LoginWithFacebook(string accessToken)
    {
        _playFabAuthentication.LoginWithFacebook(accessToken);
    }

    public void UpdateUsername(string newUsername)
    {
        if (!IsClientLoggedIn()) return;
        _playFabAuthentication.UpdateUserInfo(newUsername);
    }

    public void UpdatePlayerStatistic()
    {
        if (!IsClientLoggedIn()) return;

        _playfabLeaderboard.UpdatePlayerStatistic();
    }

    public void GetAllLeaderboardFromServer()
    {
        if (!IsClientLoggedIn()) return;

        _playfabLeaderboard.GetAllLeaderboardFromServer();
    }

    public List<PlayerLeaderboardEntry> GetLearderboard(eLeaderboardType type)
    {
        return _playfabLeaderboard.GetLeaderboard(type);
    }

    public int GetNumberOfUserOnLeaderboard()
    {
        return _playfabLeaderboard.GetNumberOfUserOnLeaderboard();
    }

    public bool IsClientLoggedIn()
    {
        return _playFabAuthentication.IsLoggedIn();
    }
}