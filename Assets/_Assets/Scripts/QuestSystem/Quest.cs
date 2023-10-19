using System;
using UnityEngine;
using UnityEngine.UI;

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

    public Quest(int questID, Sprite questIcon, Sprite prizeIcon, string questDescription, long prizeQuantity, float currentProgress, float targetProgress, eResourceDataType prizeType,
        bool isCompleted)
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
    }

    public void FinishQuest()
    {
        currentProgress = targetProgress;
        isCompleted = true;
    }

    public object Clone()
    {
        return new Quest(questID, questIcon, prizeIcon, questDescription, prizeQuantity, currentProgress, targetProgress, prizeType, isCompleted);
    }
}