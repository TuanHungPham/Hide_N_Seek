using System;
using TigerForge;
using UnityEngine;

public class UserManager : MonoBehaviour
{
    [SerializeField] private string _userName;
    private PlayfabManager PlayfabManager => PlayfabManager.Instance;

    private void Awake()
    {
        LoadingUsername();
    }

    private void Start()
    {
        ListenEvent();
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.CHANGING_USERNAME, SaveUsername);
    }

    private void LoadingUsername()
    {
        if (PlayfabManager.IsClientLoggedIn())
        {
            _userName = PlayfabManager.GetUsername();
            return;
        }

        _userName = PlayerPrefs.GetString("IN_GAME_USERNAME", "Offline");
    }

    public void SetUsername(string userName)
    {
        _userName = userName;
    }

    private void SaveUsername()
    {
        if (PlayfabManager.IsClientLoggedIn()) return;
        PlayerPrefs.SetString("IN_GAME_USERNAME", _userName);
    }

    public string GetUsername()
    {
        return _userName;
    }
}