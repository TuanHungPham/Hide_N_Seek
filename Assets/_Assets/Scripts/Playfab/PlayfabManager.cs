using PlayFab;
using UnityEngine;

public class PlayfabManager : MonoBehaviour
{
    private static PlayfabManager instance;
    public static PlayfabManager Instance => instance;

    [SerializeField] private PlayfabAuthentication _playFabAuthentication;

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
        _playFabAuthentication = GetComponentInChildren<PlayfabAuthentication>();
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

    public bool IsClientLoggedIn()
    {
        return _playFabAuthentication.IsLoggedIn();
    }
}