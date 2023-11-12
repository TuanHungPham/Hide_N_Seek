using TigerForge;
using UnityEngine;
using UnityEngine.UI;

public class AccountUIHandler : MonoBehaviour
{
    [SerializeField] private Button _accountBtn;
    [SerializeField] private Button _leaderboardBtn;
    private PlayfabManager PlayfabManager => PlayfabManager.Instance;

    private void OnEnable()
    {
        SetupButton();
        GetLeaderboardFromServer();
    }

    private void Start()
    {
        EventManager.StartListening(EventID.LOGIN_SUCCESS, SetupButton);
    }

    private void GetLeaderboardFromServer()
    {
        PlayfabManager.GetAllLeaderboardFromServer();
    }

    private void SetupButton()
    {
        if (PlayfabManager.Instance.IsClientLoggedIn())
        {
            _leaderboardBtn.interactable = true;
            _accountBtn.interactable = false;
            return;
        }

        _leaderboardBtn.interactable = false;
        _accountBtn.interactable = true;
    }
}