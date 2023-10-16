using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[Serializable]
public class QuestBaseData : BaseData
{
    public int questID;
    public bool isCompleted;

    public void AddData(QuestBaseData questBaseData)
    {
        questID = questBaseData.questID;
        isCompleted = questBaseData.isCompleted;
    }

    public void AddData(int questID, bool isCompleted)
    {
        this.questID = questID;
        this.isCompleted = isCompleted;
    }

    public override void ParseToData(string json)
    {
        QuestBaseData parsedQuestData = JsonConvert.DeserializeObject<QuestBaseData>(json);
        AddData(parsedQuestData.questID, parsedQuestData.isCompleted);
    }
}

public class QuestDataManager : MonoBehaviour
{
    [SerializeField] private List<QuestBaseData> _questBaseDataList = new List<QuestBaseData>();
    [SerializeField] private List<Quest> _questTemplateList = new List<Quest>();
    [SerializeField] private List<Quest> _todayQuestTemplateList = new List<Quest>();
    [SerializeField] private QuestTemplate _questTemplate;
    [SerializeField] private int _numberOfQuest;

    public List<QuestBaseData> QuestBaseDataList => _questBaseDataList;

    private void Awake()
    {
        LoadTemplate();
    }

    private void LoadTemplate()
    {
        foreach (var questTemplate in _questTemplate.QuestList)
        {
            Quest quest = (Quest)questTemplate.Clone();

            _questTemplateList.Add(quest);
        }

        RandomTodayQuestTemplate();
    }

    private void RandomTodayQuestTemplate()
    {
        for (int i = 0; i < _numberOfQuest; i++)
        {
            int randomIndex = Random.Range(0, _questTemplateList.Count - 1);

            Quest quest = _questTemplateList[randomIndex];

            if (_todayQuestTemplateList.Contains(quest))
            {
                i -= 1;
                continue;
            }

            _todayQuestTemplateList.Add(quest);

            QuestBaseData questBaseData = new QuestBaseData();
            questBaseData.AddData(quest.questID, quest.isCompleted);
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

    public List<Quest> GetTodayQuestTemplateList()
    {
        return _todayQuestTemplateList;
    }
}