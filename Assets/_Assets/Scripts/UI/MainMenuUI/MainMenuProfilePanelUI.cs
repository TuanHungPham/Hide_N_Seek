using System;
using TigerForge;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuProfilePanelUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _userNameText;
    [SerializeField] private Image _profileAvatar;

    private InGameManager InGameManager => InGameManager.Instance;
    private IngameDataManager IngameDataManager => IngameDataManager.Instance;

    private void Start()
    {
        ListenEvent();
    }

    private void OnEnable()
    {
        SetupProfileUI();
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.CHANGING_USERNAME, SetupUsernameUI);
        EventManager.StartListening(EventID.SETTING_UP_PROFILE, SetupUsernameUI);
        EventManager.StartListening(EventID.CHOOSING_ITEM_SHOP, SetupProfileAvatar);
    }

    private void SetupProfileUI()
    {
        SetupUsernameUI();
        SetupProfileAvatar();
    }

    private void SetupUsernameUI()
    {
        _userNameText.text = InGameManager.GetUsername();
    }

    private void SetupProfileAvatar()
    {
        ItemShop currentSelectedCostume = IngameDataManager.GetCurrentSelectedCostumeItemShop();
        _profileAvatar.sprite = currentSelectedCostume.GetItemImage();
    }
}