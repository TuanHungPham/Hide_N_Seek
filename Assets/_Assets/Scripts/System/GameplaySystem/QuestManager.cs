using System.Collections.Generic;
using TigerForge;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private List<Quest> _todayQuestList;

    private void Start()
    {
        InitializeTodayQuestList();
    }

    public void InitializeTodayQuestList()
    {
        _todayQuestList = IngameDataManager.Instance.GetTodayQuestTemplateList();
    }

    public void UpdateQuest()
    {
    }

    public void FinishQuest(int questID)
    {
        Quest quest = _todayQuestList.Find(x => x.questID == questID);
        if (quest == null) return;

        quest.FinishQuest();
        EmitUpdatingQuestEvent();
    }

    public List<Quest> GetTodayQuestList()
    {
        return _todayQuestList;
    }

    private void EmitUpdatingQuestEvent()
    {
        EventManager.EmitEvent(EventID.QUEST_UPDATING);
    }
}