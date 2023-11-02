using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class PlayfabAuthentication : MonoBehaviour
{
    public void LoginWithEmail(string email, string password)
    {
        LoginWithEmailAddressRequest request = new LoginWithEmailAddressRequest()
        {
            Email = email,
            Password = password,
        };

        PlayFabClientAPI.LoginWithEmailAddress(request, OnAuthCallback, OnAuthError);
    }

    public void LoginWithFacebook(string accessToken)
    {
        LoginWithFacebookRequest request = new LoginWithFacebookRequest()
        {
            AccessToken = accessToken,
            CreateAccount = true,
        };

        PlayFabClientAPI.LoginWithFacebook(request, OnAuthCallback, OnAuthError);
    }

    private void OnAuthError(PlayFabError error)
    {
        Debug.LogError($"--- (PLAYFAB) Login Error: {error.ErrorMessage}");
    }

    private void OnAuthCallback(LoginResult result)
    {
        Debug.Log($"--- (PLAYFAB) Login Result: {result.PlayFabId}");
    }

    public void SignUpWithEmail(string email, string password, string displayName = "")
    {
        RegisterPlayFabUserRequest request = new RegisterPlayFabUserRequest()
        {
            Email = email,
            Password = password,
            DisplayName = displayName,
            RequireBothUsernameAndEmail = false
        };

        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterResult, OnAuthError);
    }

    private void OnRegisterResult(RegisterPlayFabUserResult result)
    {
        Debug.Log($"--- (PLAYFAB) Sign Up Result: {result.PlayFabId} --- {result.EntityToken}");
    }
}