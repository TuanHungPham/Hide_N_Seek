using System.Collections.Generic;
using Facebook.Unity;
using UnityEngine;

public class FacebookAuth : MonoBehaviour
{
    private string _username;
    private PlayfabManager PlayfabManager => PlayfabManager.Instance;
    private InGameManager InGameManager => InGameManager.Instance;

    public void LoginFacebook()
    {
        var perms = new List<string>()
        {
            "public_profile",
            "email"
        };
        FB.LogInWithReadPermissions(perms, AuthCallback);
    }

    private void AuthCallback(ILoginResult result)
    {
        if (result.Error != null)
        {
            Debug.Log($"(Facebook) Error Logging in Facebook: {result.Error}");
            return;
        }

        if (FB.IsLoggedIn)
        {
            string accessToken = result.AccessToken.TokenString;
            GetUserInformation();
            PlayfabManager.LoginWithFacebook(accessToken);
        }
        else
        {
            Debug.Log("(Facebook) User cancelled login");
        }
    }

    private void GetUserInformation()
    {
        string query = "/me?fields=id,name";
        FB.API(query, HttpMethod.GET, OnResultCallback);
    }

    private void OnResultCallback(IGraphResult result)
    {
        _username = result.ResultDictionary["name"].ToString();

        InGameManager.SetUsername(_username);
    }

    public string GetUsername()
    {
        return _username;
    }
}