using System.Collections.Generic;
using TigerForge;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private List<Quest> _todayQuestList;
    [SerializeField] private SpecialQuest _todaySpecialQuest;
    private InGameManager InGameManager => InGameManager.Instance;
    private GameplayManager GameplayManager => GameplayManager.Instance;
    private IngameDataManager IngameDataManager => IngameDataManager.Instance;

    private void Awake()
    {
        ListenEvent();
    }

    private void Start()
    {
        InitializeTodayQuestList();
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.QUEST_RESETTING, InitializeTodayQuestList);
    }

    private void InitializeTodayQuestList()
    {
        _todayQuestList = IngameDataManager.GetTodayQuestTemplateList();
        _todaySpecialQuest = IngameDataManager.GetTodaySpecialQuest();
    }

    public void UpdateQuestProgress(eQuestType questType, eAchievementType achievementType)
    {
        var quest = _todayQuestList.Find(x => x.questType == questType);
        if (quest == null) return;

        var newProgress = quest.currentProgress + InGameManager.GetAchievementPoint(achievementType);

        if (!CanUpdateQuest(quest)) return;

        quest.UpdateQuest(newProgress);

        CheckQuestState(quest);
    }

    private void UpdateSpecialQuestProgress()
    {
        if (_todaySpecialQuest.isCompleted) return;

        _todaySpecialQuest.currentProgress++;
        CheckQuestState(_todaySpecialQuest);
    }

    private bool CanUpdateQuest(Quest quest)
    {
        if (quest.isCompleted) return false;
        if (quest.currentProgress >= quest.targetProgress) return false;

        return true;
    }

    public void FinishQuest(int questID)
    {
        var quest = _todayQuestList.Find(x => x.questID == questID);
        if (quest == null) return;

        quest.FinishQuest();
        RewardQuestPrize(quest);
        UpdateSpecialQuestProgress();
        EmitUpdatingQuestEvent();
    }

    public List<Quest> GetTodayQuestList()
    {
        return _todayQuestList;
    }

    public SpecialQuest GetTodaySpecialQuest()
    {
        return _todaySpecialQuest;
    }

    private void CheckQuestState(Quest quest)
    {
        if (quest.currentProgress >= quest.targetProgress)
        {
            quest.isCompleted = true;
            RewardQuestPrize(quest);
            UpdateSpecialQuestProgress();
        }
        else
        {
            quest.isCompleted = false;
        }

        EmitUpdatingQuestEvent();
    }

    private void CheckQuestState(SpecialQuest specialQuest)
    {
        if (specialQuest.currentProgress >= specialQuest.targetProgress)
        {
            specialQuest.isCompleted = true;
            RewardQuestPrize(specialQuest);
        }
        else
        {
            specialQuest.isCompleted = false;
        }
    }

    private void RewardQuestPrize(Quest quest)
    {
        var prizeType = quest.prizeType;
        var prizeQuantity = quest.prizeQuantity;

        switch (prizeType)
        {
            case eResourceDataType.COIN:
                GameplayManager.AddCoin(prizeQuantity);
                break;
            case eResourceDataType.ADS_TICKET:
                GameplayManager.AddTicket(prizeQuantity);
                break;
        }
    }

    private void RewardQuestPrize(SpecialQuest specialQuest)
    {
        var coinPrizeQuantity = specialQuest.coinPrizeQuantity;
        var ticketPrizeQuantity = specialQuest.ticketPrizeQuantity;

        GameplayManager.AddCoin(coinPrizeQuantity);
        GameplayManager.AddTicket(ticketPrizeQuantity);
    }

    private void EmitUpdatingQuestEvent()
    {
        EventManager.EmitEvent(EventID.QUEST_UPDATING);
    }
}