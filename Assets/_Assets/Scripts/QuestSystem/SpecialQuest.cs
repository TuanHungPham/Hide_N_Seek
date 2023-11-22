using System;
using UnityEngine;

[Serializable]
public class SpecialQuest : ICloneable
{
    public int questID;
    public Sprite ticketPrizeIcon;
    public Sprite coinPrizeIcon;
    public long ticketPrizeQuantity;
    public long coinPrizeQuantity;
    public float currentProgress;
    public float targetProgress;
    public bool isCompleted;

    public SpecialQuest(int questID, Sprite ticketPrizeIcon, Sprite coinPrizeIcon, long ticketPrizeQuantity, long coinPrizeQuantity,
        float currentProgress, float targetProgress, bool isCompleted)
    {
        this.questID = questID;
        this.ticketPrizeIcon = ticketPrizeIcon;
        this.coinPrizeIcon = coinPrizeIcon;
        this.ticketPrizeQuantity = ticketPrizeQuantity;
        this.coinPrizeQuantity = coinPrizeQuantity;
        this.currentProgress = currentProgress;
        this.targetProgress = targetProgress;
        this.isCompleted = isCompleted;
    }

    public object Clone()
    {
        return new SpecialQuest(questID, ticketPrizeIcon, coinPrizeIcon, ticketPrizeQuantity, coinPrizeQuantity, currentProgress, targetProgress, isCompleted);
    }
}