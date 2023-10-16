using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestPanelUI : MonoBehaviour
{
    [SerializeField] private int _maxQuestNumber;
    [SerializeField] private GameObject _questUIPrefab;
    [SerializeField] private List<QuestUI> _questUiList = new List<QuestUI>();
    [SerializeField] private QuestTemplate _questTemplate;

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
        List<Quest> questTemplateList = _questTemplate.QuestList;
        for (int i = 0; i < _maxQuestNumber; i++)
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