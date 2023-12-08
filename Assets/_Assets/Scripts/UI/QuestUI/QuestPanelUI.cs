using System;
using System.Collections.Generic;
using TigerForge;
using UnityEngine;

public class QuestPanelUI : MonoBehaviour
{
    [SerializeField] private GameObject _questUIPrefab;
    [SerializeField] private List<QuestUI> _questUiList = new List<QuestUI>();

    private InGameManager InGameManager => InGameManager.Instance;

    private void Awake()
    {
        InitializeQuestUIList();
    }

    private void Start()
    {
        ListenEvent();
    }

    private void OnEnable()
    {
        ResetQuestUIList();
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.QUEST_RESETTING, ResetQuestUIList);
    }

    private void InitializeQuestUIList()
    {
        List<Quest> todayQuestList = InGameManager.GetTodayQuestList();
        for (int i = 0; i < todayQuestList.Count; i++)
        {
            GameObject questUI;
            questUI = Instantiate(_questUIPrefab, transform, true);

            QuestUI questUIScript = questUI.GetComponent<QuestUI>();
            _questUiList.Add(questUIScript);

            Quest questTemplate = todayQuestList[i];
            SetupQuestUI(questUIScript, questTemplate);
        }
    }

    private void ResetQuestUIList()
    {
        List<Quest> todayQuestList = InGameManager.GetTodayQuestList();
        for (int i = 0; i < todayQuestList.Count; i++)
        {
            GameObject questUI = _questUiList[i].gameObject;
            QuestUI questUIScript = questUI.GetComponent<QuestUI>();

            Quest questTemplate = todayQuestList[i];
            SetupQuestUI(questUIScript, questTemplate);
        }
    }

    private void SetupQuestUI(QuestUI questUI, Quest quest)
    {
        if (questUI == null) return;

        questUI.SetUIData(quest);
    }
}