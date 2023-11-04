using PlayFab;
using PlayFab.ClientModels;
using TigerForge;
using UnityEngine;

public class PlayfabAuthentication : MonoBehaviour
{
    private AuthHandler AuthHandler => AuthHandler.Instance;

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
        AuthHandler.ShowError(error.ErrorMessage);
        EmitLoginFailEvent();
    }

    private void OnAuthCallback(LoginResult result)
    {
        Debug.Log($"--- (PLAYFAB) Login Result: {result.PlayFabId}");
        EmitLoginSuccessEvent();
    }

    public void SignUpWithEmail(string email, string password, string displayName)
    {
        RegisterPlayFabUserRequest request = new RegisterPlayFabUserRequest()
        {
            Email = email,
            Password = password,
            Username = displayName,
            RequireBothUsernameAndEmail = true
        };

        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterResult, OnSignUpError);
    }

    private void OnSignUpError(PlayFabError error)
    {
        Debug.LogError($"--- (PLAYFAB) Sign Up Error: {error.ErrorMessage}");
        AuthHandler.ShowError(error.ErrorMessage);
    }

    private void OnRegisterResult(RegisterPlayFabUserResult result)
    {
        Debug.Log($"--- (PLAYFAB) Sign Up Result: {result.PlayFabId} --- {result.EntityToken}");
    }

    private void EmitLoginSuccessEvent()
    {
        EventManager.EmitEvent(EventID.LOGIN_SUCCESS);
    }

    private void EmitLoginFailEvent()
    {
        EventManager.EmitEvent(EventID.LOGIN_FAIL);
    }
}