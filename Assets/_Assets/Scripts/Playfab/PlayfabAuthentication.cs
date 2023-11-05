using PlayFab;
using PlayFab.ClientModels;
using PlayFab.ProfilesModels;
using TigerForge;
using UnityEngine;

public class PlayfabAuthentication : MonoBehaviour
{
    private AuthHandler AuthHandler => AuthHandler.Instance;
    private InGameManager InGameManager => InGameManager.Instance;

    public void LoginWithEmail(string email, string password)
    {
        LoginWithEmailAddressRequest request = new LoginWithEmailAddressRequest()
        {
            Email = email,
            Password = password,
        };

        PlayFabClientAPI.LoginWithEmailAddress(request, OnAuthCallback, OnAuthError);
    }

    public void LoginWithID(PlayFabAuthenticationContext context)
    {
        LoginWithPlayFabRequest request = new LoginWithPlayFabRequest()
        {
            AuthenticationContext = context
        };
        // LoginWithCustomIDRequest request = new LoginWithCustomIDRequest()
        // {
        //
        //     AuthenticationContext = 
        // };

        PlayFabClientAPI.LoginWithPlayFab(request, OnAuthCallback, OnAuthError);
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
        SetupUserInfo();
        EmitLoginSuccessEvent();
    }

    public void SignUpWithEmail(string email, string password, string displayName)
    {
        RegisterPlayFabUserRequest request = new RegisterPlayFabUserRequest()
        {
            Email = email,
            Password = password,
            Username = displayName,
            DisplayName = displayName,
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
        Debug.Log($"--- (PLAYFAB) Sign Up Result: {result.PlayFabId} --- {result.AuthenticationContext.EntityId}");
        LoginWithID(result.AuthenticationContext);
    }

    private void SetupUserInfo()
    {
        if (!PlayFabClientAPI.IsClientLoggedIn()) return;

        GetPlayerProfileRequest request = new GetPlayerProfileRequest();
        PlayFabClientAPI.GetPlayerProfile(request, OnGetUserProfileCallback, OnErrorCallback);
    }

    private void OnGetUserProfileCallback(GetPlayerProfileResult result)
    {
        string displayName = result.PlayerProfile.DisplayName;
        if (string.IsNullOrEmpty(displayName))
        {
            displayName = InGameManager.GetUsername();
            UpdateUserInfo(displayName);
        }
        else
        {
            InGameManager.SetUsername(displayName);
        }

        EmitSetupUserProfileEvent();
    }

    private void UpdateUserInfo(string displayName)
    {
        UpdateUserTitleDisplayNameRequest request = new UpdateUserTitleDisplayNameRequest()
        {
            DisplayName = displayName
        };

        PlayFabClientAPI.UpdateUserTitleDisplayName(request, UpdateUserInfoCallback, OnErrorCallback);
    }

    private void UpdateUserInfoCallback(UpdateUserTitleDisplayNameResult result)
    {
        Debug.LogError($"--- (PLAYFAB) Update User Info: {result.DisplayName}");
    }

    private void OnErrorCallback(PlayFabError error)
    {
        Debug.LogError($"--- (PLAYFAB) Error: {error.ErrorMessage}");
    }

    private void EmitSetupUserProfileEvent()
    {
        EventManager.EmitEvent(EventID.SETTING_UP_PROFILE);
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