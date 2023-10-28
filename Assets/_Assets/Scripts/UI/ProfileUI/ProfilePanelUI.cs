using TigerForge;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfilePanelUI : MonoBehaviour
{
    [Header("TEXT")] [SerializeField] private TMP_Text _levelText;
    [SerializeField] private TMP_Text _foundPlayersText;
    [SerializeField] private TMP_Text _rescuedPlayersText;
    [SerializeField] private TMP_InputField _nameInput;

    [Header("IMAGE")] [SerializeField] private Image _profileAvatar;
    private InGameManager InGameManager => InGameManager.Instance;

    private void OnEnable()
    {
        SetupUI();
    }

    private void SetupUI()
    {
        _levelText.text = InGameManager.GetAchievementPoint(eAchievementType.WINNING_TIME).ToString();
        _foundPlayersText.text = InGameManager.GetAchievementPoint(eAchievementType.CATCHING_TIME).ToString();
        _rescuedPlayersText.text = InGameManager.GetAchievementPoint(eAchievementType.RESCUING_TIME).ToString();
        _nameInput.text = InGameManager.GetUsername();

        SetupProfileAvatar();
    }

    public void SetUsernameByNameInput(string nameInput)
    {
        InGameManager.SetUsername(nameInput);
        EmitChangingUsernameEvent();
    }

    private void SetupProfileAvatar()
    {
        ItemShop currentSelectedCostume = IngameDataManager.Instance.GetCurrentSelectedCostumeItemShop();
        _profileAvatar.sprite = currentSelectedCostume.GetItemImage();
    }

    public void EmitChangingUsernameEvent()
    {
        EventManager.EmitEvent(EventID.CHANGING_USERNAME);
    }
}