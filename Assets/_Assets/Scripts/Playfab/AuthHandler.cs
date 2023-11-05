using System;
using TMPro;
using UnityEngine;

public class AuthHandler : MonoBehaviour
{
    [Header("LOGIN")] [SerializeField] private TMP_InputField login_emailInput;

    [SerializeField] private TMP_InputField login_passwordInput;

    [Space(20)] [Header("SIGN UP")] [SerializeField]
    private TMP_InputField signUp_emailInput;

    [SerializeField] private TMP_InputField signUp_passwordInput;
    [SerializeField] private TMP_InputField signUp_repasswordInput;
    [SerializeField] private TMP_InputField signUp_displayNameInput;

    [Space(20)] [Header("ERROR")] [SerializeField]
    private ErrorPanel _errorPanel;

    private PlayfabManager PlayfabManager => PlayfabManager.Instance;
    private FacebookManager FacebookManager => FacebookManager.Instance;

    public static AuthHandler Instance => instance;

    private static AuthHandler instance;

    private void OnEnable()
    {
        instance = this;
    }

    public void Login()
    {
        string email = login_emailInput.text;
        string password = login_passwordInput.text;

        PlayfabManager.LoginWithEmail(email, password);
    }

    public void SignUp()
    {
        string email = signUp_emailInput.text;
        string password = signUp_passwordInput.text;
        string re_password = signUp_repasswordInput.text;
        string displayName = signUp_displayNameInput.text;

        if (!password.Equals(re_password)) return;

        PlayfabManager.SignUpWithEmail(email, password, displayName);
    }

    public void LogInFacebook()
    {
        FacebookManager.LoginFacebook();
    }

    public void ShowError(string error)
    {
        _errorPanel.ShowErrorText(error);
    }

    private void OnDisable()
    {
        instance = null;
    }
}