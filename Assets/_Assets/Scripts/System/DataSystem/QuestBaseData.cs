using System;
using Newtonsoft.Json;

[Serializable]
public class QuestBaseData : BaseData
{
    public int questID;
    public float currentProgress;
    public float targetProgress;
    public bool isCompleted;

    public void AddData(QuestBaseData questBaseData)
    {
        questID = questBaseData.questID;
        currentProgress = questBaseData.currentProgress;
        targetProgress = questBaseData.targetProgress;
        isCompleted = questBaseData.isCompleted;
    }

    public void AddData(int questID, float currentProgress, float targetProgress, bool isCompleted)
    {
        this.questID = questID;
        this.currentProgress = currentProgress;
        this.targetProgress = targetProgress;
        this.isCompleted = isCompleted;
    }

    public void ModifyData(float currentProgress, float targetProgress, bool isCompleted)
    {
        this.currentProgress = currentProgress;
        this.targetProgress = targetProgress;
        this.isCompleted = isCompleted;
    }

    public override void ParseToData(string json)
    {
        QuestBaseData parsedQuestData = JsonConvert.DeserializeObject<QuestBaseData>(json);
        AddData(parsedQuestData.questID, parsedQuestData.currentProgress, parsedQuestData.targetProgress, parsedQuestData.isCompleted);
    }
}