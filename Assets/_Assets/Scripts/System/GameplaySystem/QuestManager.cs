using System.Collections.Generic;
using TigerForge;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private List<Quest> _todayQuestList;
    private InGameManager InGameManager => InGameManager.Instance;

    private void Start()
    {
        InitializeTodayQuestList();
    }

    public void InitializeTodayQuestList()
    {
        _todayQuestList = IngameDataManager.Instance.GetTodayQuestTemplateList();
    }

    public void UpdateQuestProgress(eQuestType questType, eAchievementType achievementType)
    {
        Quest quest = _todayQuestList.Find(x => x.questType == questType);
        if (quest == null) return;

        quest.currentProgress += InGameManager.GetAchievementPoint(achievementType);
        CheckQuestState(quest);
        EmitUpdatingQuestEvent();
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

    private void CheckQuestState(Quest quest)
    {
        if (quest.currentProgress == quest.targetProgress)
        {
            quest.isCompleted = true;
            return;
        }

        quest.isCompleted = false;
    }

    private void EmitUpdatingQuestEvent()
    {
        EventManager.EmitEvent(EventID.QUEST_UPDATING);
    }
}