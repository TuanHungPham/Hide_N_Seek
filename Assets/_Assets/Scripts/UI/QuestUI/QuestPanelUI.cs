using System.Collections.Generic;
using UnityEngine;

public class QuestPanelUI : MonoBehaviour
{
    [SerializeField] private GameObject _questUIPrefab;
    [SerializeField] private List<QuestUI> _questUiList = new List<QuestUI>();

    private InGameManager InGameManager => InGameManager.Instance;

    private void Awake()
    {
        LoadComponents();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
    }

    private void Start()
    {
        InitializeQuestUIList();
    }

    private void InitializeQuestUIList()
    {
        List<Quest> todayQuestList = InGameManager.GetTodayQuestList();
        for (int i = 0; i < todayQuestList.Count; i++)
        {
            GameObject questUI = Instantiate(_questUIPrefab, transform, true);
            Quest questTemplate = (Quest)todayQuestList[i].Clone();
            SetupQuestUI(questUI, questTemplate);
        }
    }

    private void SetupQuestUI(GameObject _questUI, Quest quest)
    {
        QuestUI questUI = _questUI.GetComponent<QuestUI>();
        if (questUI == null) return;

        _questUiList.Add(questUI);
        questUI.SetUIData(quest);
    }
}