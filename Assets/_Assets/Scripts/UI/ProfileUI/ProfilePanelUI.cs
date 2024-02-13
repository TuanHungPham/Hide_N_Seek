using TigerForge;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfilePanelUI : MonoBehaviour
{
    [SerializeField] private bool _isNameModifed;

    [Header("TEXT")] [SerializeField] private TMP_Text _levelText;
    [SerializeField] private TMP_Text _foundPlayersText;
    [SerializeField] private TMP_Text _rescuedPlayersText;
    [SerializeField] private TMP_InputField _nameInput;

    [Header("IMAGE")] [SerializeField] private Image _profileAvatar;
    private IngameDataManager IngameDataManager => IngameDataManager.Instance;
    private InGameManager InGameManager => InGameManager.Instance;

    private void OnEnable()
    {
        SetupUI();
        _isNameModifed = false;
    }

    private void OnDisable()
    {
        EmitChangingUsernameEvent();
    }

    private void SetupUI()
    {
        _levelText.text = IngameDataManager.GetAchievementData(eAchievementType.WINNING_TIME).ToString();
        _foundPlayersText.text = IngameDataManager.GetAchievementData(eAchievementType.CATCHING_TIME).ToString();
        _rescuedPlayersText.text = IngameDataManager.GetAchievementData(eAchievementType.RESCUING_TIME).ToString();
        _nameInput.text = InGameManager.GetUsername();

        SetupProfileAvatar();
    }

    public void SetUsernameByNameInput(string nameInput)
    {
        var oldUsername = InGameManager.GetUsername();

        if (oldUsername.Equals(nameInput) || string.IsNullOrEmpty(nameInput)) return;
        InGameManager.SetUsername(nameInput);
        _isNameModifed = true;
    }

    private void SetupProfileAvatar()
    {
        var currentSelectedCostume = IngameDataManager.Instance.GetCurrentSelectedCostumeItemShop();
        _profileAvatar.sprite = currentSelectedCostume.GetItemImage();
    }

    private void EmitChangingUsernameEvent()
    {
        if (!_isNameModifed) return;

        Debug.Log("Changing username...");
        PlayfabManager.Instance.UpdateUsername(_nameInput.text);
        EventManager.EmitEvent(EventID.CHANGING_USERNAME);
    }
}