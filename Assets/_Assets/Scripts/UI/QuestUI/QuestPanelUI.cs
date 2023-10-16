using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestPanelUI : MonoBehaviour
{
    [SerializeField] private GameObject _questUIPrefab;
    [SerializeField] private List<QuestUI> _questUiList = new List<QuestUI>();
    [SerializeField] private QuestTemplate _questTemplate;

    private IngameDataManager InGameDataManager => IngameDataManager.Instance;

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
        List<Quest> questTemplateList = InGameDataManager.GetTodayQuestTemplateList();
        for (int i = 0; i < questTemplateList.Count; i++)
        {
            GameObject questUI = Instantiate(_questUIPrefab, transform, true);
            Quest questTemplate = (Quest)questTemplateList[i].Clone();
            SetupQuestUI(questUI, questTemplate);
        }
    }

    private void SetupQuestUI(GameObject _questUI, Quest quest)
    {
        QuestUI questUI = _questUI.GetComponent<QuestUI>();

        questUI.SetUIData(quest);
    }
}