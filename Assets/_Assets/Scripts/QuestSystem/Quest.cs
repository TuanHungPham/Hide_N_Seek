using System;
using UnityEngine;

public enum eQuestType
{
    WINNING,
    RESCUING,
    CATCHING,
    COMPLETION
}

[Serializable]
public class Quest : ICloneable
{
    public int questID;
    public Sprite questIcon;
    public Sprite prizeIcon;
    public string questDescription;
    public long prizeQuantity;
    public float currentProgress;
    public float targetProgress;
    public eResourceDataType prizeType;
    public bool isCompleted;
    public eQuestType questType;

    public Quest(int questID, Sprite questIcon, Sprite prizeIcon, string questDescription, long prizeQuantity, float currentProgress, float targetProgress, eResourceDataType prizeType,
        bool isCompleted, eQuestType questType)
    {
        this.questID = questID;
        this.questIcon = questIcon;
        this.prizeIcon = prizeIcon;
        this.questDescription = questDescription;
        this.prizeQuantity = prizeQuantity;
        this.currentProgress = currentProgress;
        this.targetProgress = targetProgress;
        this.prizeType = prizeType;
        this.isCompleted = isCompleted;
        this.questType = questType;
    }

    public object Clone()
    {
        return new Quest(questID, questIcon, prizeIcon, questDescription, prizeQuantity, currentProgress, targetProgress, prizeType, isCompleted, questType);
    }

    public void UpdateQuest(float quantity)
    {
        currentProgress = quantity;

        if (currentProgress > targetProgress) currentProgress = targetProgress;
    }

    public void FinishQuest()
    {
        currentProgress = targetProgress;
        isCompleted = true;
    }
}