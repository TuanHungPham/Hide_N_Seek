using System.Collections.Generic;
using PlayFab.ClientModels;
using UnityEngine;

public class PlayfabManager : PermanentMonoSingleton<PlayfabManager>
{
    [SerializeField] private PlayfabAuthentication _playFabAuthentication;
    [SerializeField] private PlayfabLeaderboard _playfabLeaderboard;
    [SerializeField] private PlayfabDataLoader _playfabDataLoader;

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
        _playFabAuthentication = GetComponentInChildren<PlayfabAuthentication>();
        _playfabLeaderboard = GetComponentInChildren<PlayfabLeaderboard>();
        _playfabDataLoader = GetComponentInChildren<PlayfabDataLoader>();
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

    public void RecoverPassword(string email)
    {
        _playFabAuthentication.RecoverPassword(email);
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

    public void AddDataToSaveCache(eDataType type, string data)
    {
        _playfabDataLoader.AddDataToSaveCache(type, data);
    }

    public void SaveDataToServer()
    {
        _playfabDataLoader.SaveDataToServer();
    }

    public void LoadDataFromServer()
    {
        _playfabDataLoader.LoadDataFromServer();
    }

    public string GetUserData(eDataType type)
    {
        return _playfabDataLoader.GetUserData(type);
    }

    public string GetUsername()
    {
        return _playFabAuthentication.GetUsername();
    }
}