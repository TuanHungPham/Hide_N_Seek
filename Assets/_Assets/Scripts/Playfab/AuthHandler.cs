using TMPro;
using UnityEngine;

public class AuthHandler : MonoBehaviour
{
    [SerializeField] private NotiPanel _notiPanel;
    [SerializeField] private GameObject _authPanel;
    [SerializeField] private GameObject _loginPanel;
    [SerializeField] private GameObject _signUpPanel;

    [Space(20)] [Header("LOGIN")] [SerializeField]
    private TMP_InputField login_emailInput;

    [SerializeField] private TMP_InputField login_passwordInput;

    [Space(20)] [Header("SIGN UP")] [SerializeField]
    private TMP_InputField signUp_emailInput;

    [SerializeField] private TMP_InputField signUp_passwordInput;
    [SerializeField] private TMP_InputField signUp_repasswordInput;
    [SerializeField] private TMP_InputField signUp_displayNameInput;

    private PlayfabManager PlayfabManager => PlayfabManager.Instance;
    private FacebookManager FacebookManager => FacebookManager.Instance;

    public static AuthHandler Instance => instance;

    private static AuthHandler instance;

    private void OnEnable()
    {
        instance = this;
        ResetUI();
    }

    private void ResetUI()
    {
        ResetPanelUI();
        ResetInputUI();
    }

    private void ResetPanelUI()
    {
        _authPanel.gameObject.SetActive(true);
        _loginPanel.gameObject.SetActive(false);
        _signUpPanel.gameObject.SetActive(false);
    }

    private void ResetInputUI()
    {
        login_emailInput.text = "";
        login_passwordInput.text = "";
        signUp_emailInput.text = "";
        signUp_passwordInput.text = "";
        signUp_repasswordInput.text = "";
        signUp_displayNameInput.text = "";
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

    public void ShowNotification(string noti, eNotiType notiType)
    {
        _notiPanel.ShowNotification(noti, notiType);
    }

    public void LogInFacebook()
    {
        FacebookManager.LoginFacebook();
    }

    private void OnDisable()
    {
        instance = null;
    }
}