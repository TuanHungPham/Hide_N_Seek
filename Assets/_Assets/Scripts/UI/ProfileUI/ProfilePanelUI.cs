using TigerForge;
using TMPro;
using UnityEngine;

public class ProfilePanelUI : MonoBehaviour
{
    [Header("TEXT")] [SerializeField] private TMP_Text _levelText;
    [SerializeField] private TMP_Text _foundPlayersText;
    [SerializeField] private TMP_Text _rescuedPlayersText;
    [SerializeField] private TMP_InputField _nameInput;
    private InGameManager InGameManager => InGameManager.Instance;

    private void OnEnable()
    {
        SetupUI();
    }

    private void SetupUI()
    {
        _levelText.text = InGameManager.GetAchievementPointData(eAchievementDataType.LEVEL).ToString();
        _foundPlayersText.text = InGameManager.GetAchievementPointData(eAchievementDataType.FOUND_PLAYERS).ToString();
        _rescuedPlayersText.text = InGameManager.GetAchievementPointData(eAchievementDataType.RESCUED_PLAYERS).ToString();
        _nameInput.text = InGameManager.GetUsername();
    }

    public void SetUsernameByNameInput(string nameInput)
    {
        InGameManager.SetUsername(nameInput);
        EmitChangingUsernameEvent();
    }

    public void EmitChangingUsernameEvent()
    {
        EventManager.EmitEvent(EventID.CHANGING_USERNAME);
    }
}