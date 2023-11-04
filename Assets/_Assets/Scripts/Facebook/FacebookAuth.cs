using System.Collections.Generic;
using Facebook.Unity;
using UnityEngine;

public class FacebookAuth : MonoBehaviour
{
    private PlayfabManager PlayfabManager => PlayfabManager.Instance;

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
            PlayfabManager.Instance.LoginWithFacebook(accessToken);
        }
        else
        {
            Debug.Log("(Facebook) User cancelled login");
        }
    }
}