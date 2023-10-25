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
        EventManager.StartListening(EventID.CHOOSING_ITEM_SHOP, SetupProfileAvatar);
    }

    private void SetupProfileUI()
    {
        SetupUsernameUI();
        SetupProfileAvatar();
    }

    private void SetupUsernameUI()
    {
        _userNameText.text = InGameManager.Instance.GetUsername();
    }

    private void SetupProfileAvatar()
    {
        ItemShop currentSelectedCostume = IngameDataManager.Instance.GetCurrentSelectedCostumeItemShop();
        _profileAvatar.sprite = currentSelectedCostume.GetItemImage();
    }
}