using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TigerForge;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

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

public class QuestDataManager : MonoBehaviour
{
    [SerializeField] private List<QuestBaseData> _questBaseDataList = new List<QuestBaseData>();
    [SerializeField] private List<Quest> _todayQuestTemplateList = new List<Quest>();
    [SerializeField] private QuestTemplate _questTemplate;
    [SerializeField] private int _numberOfQuest;

    public List<QuestBaseData> QuestBaseDataList => _questBaseDataList;

    private void Awake()
    {
        LoadTemplate();
    }

    private void Start()
    {
        ListenEvent();
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.QUEST_UPDATING, UpdateBaseData);
    }

    private void LoadTemplate()
    {
        List<Quest> questTemplateList = new List<Quest>();

        foreach (var questTemplate in _questTemplate.QuestList)
        {
            Quest quest = (Quest)questTemplate.Clone();

            questTemplateList.Add(quest);
        }

        RandomTodayQuestTemplate(questTemplateList);
    }

    private void RandomTodayQuestTemplate(List<Quest> questTemplateList)
    {
        for (int i = 0; i < _numberOfQuest; i++)
        {
            int randomIndex = Random.Range(0, questTemplateList.Count - 1);

            Quest quest = questTemplateList[randomIndex];

            if (_todayQuestTemplateList.Contains(quest))
            {
                i -= 1;
                continue;
            }

            _todayQuestTemplateList.Add(quest);

            QuestBaseData questBaseData = new QuestBaseData();
            questBaseData.AddData(quest.questID, quest.currentProgress, quest.targetProgress, quest.isCompleted);
            AddBaseData(questBaseData);
        }
    }

    public void AddBaseData(QuestBaseData questBaseData)
    {
        QuestBaseData existedBaseData = QuestBaseDataList.Find(x => x.questID == questBaseData.questID);

        if (existedBaseData == null)
        {
            QuestBaseDataList.Add(questBaseData);
        }
        else
        {
            existedBaseData.AddData(questBaseData);
        }
    }

    private void UpdateBaseData()
    {
        for (int i = 0; i < _todayQuestTemplateList.Count; i++)
        {
            Quest quest = _todayQuestTemplateList[i];
            QuestBaseData questBaseData = _questBaseDataList[i];

            questBaseData.ModifyData(quest.currentProgress, quest.targetProgress, quest.isCompleted);
        }
    }

    public List<Quest> GetTodayQuestTemplateList()
    {
        return _todayQuestTemplateList;
    }
}