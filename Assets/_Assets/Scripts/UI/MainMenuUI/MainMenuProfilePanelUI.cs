using TigerForge;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuProfilePanelUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _userNameText;
    [SerializeField] private Image _profileAvatar;

    private void Start()
    {
        ListenEvent();
        SetupProfileUI();
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.CHANGING_USERNAME, SetupUsernameUI);
    }

    private void SetupProfileUI()
    {
        SetupUsernameUI();
    }

    private void SetupUsernameUI()
    {
        _userNameText.text = InGameManager.Instance.GetUsername();
    }
}