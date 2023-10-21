using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TigerForge;
using UnityEngine;
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
    private List<Quest> _questTemplateList = new List<Quest>();
    private long _initTime;
    private TimeSpan _resetDelayTimeSpan = TimeSpan.FromHours(24);


    public List<QuestBaseData> QuestBaseDataList => _questBaseDataList;

    private void Awake()
    {
        LoadTime();
        LoadTemplate();
    }

    private void LoadTime()
    {
        _initTime = long.Parse(PlayerPrefs.GetString("QUEST_INIT_TIME", "0"));
    }

    private void SaveTime()
    {
        PlayerPrefs.SetString("QUEST_INIT_TIME", _initTime.ToString());
    }

    private void Start()
    {
        ListenEvent();
        LoadData();
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.QUEST_UPDATING, UpdateBaseData);
    }

    private void LoadTemplate()
    {
        foreach (var questTemplate in _questTemplate.QuestList)
        {
            Quest quest = (Quest)questTemplate.Clone();

            _questTemplateList.Add(quest);
        }
    }

    public void LoadData()
    {
        if (QuestBaseDataList.Count <= 0 || IsNewDay())
        {
            RandomTodayQuestTemplate();
            return;
        }

        Debug.Log($"(QUEST) Loading quest list...");

        for (int i = 0; i < QuestBaseDataList.Count; i++)
        {
            int questID = QuestBaseDataList[i].questID;
            float currentProgress = QuestBaseDataList[i].currentProgress;
            bool isCompleted = QuestBaseDataList[i].isCompleted;

            Quest quest = _questTemplateList.Find(x => x.questID == questID);
            if (quest == null) return;

            quest.currentProgress = currentProgress;
            quest.isCompleted = isCompleted;

            _todayQuestTemplateList.Add(quest);
        }
    }

    public void RandomTodayQuestTemplate()
    {
        if (QuestBaseDataList.Count > 0)
        {
            QuestBaseDataList.Clear();
        }

        if (_todayQuestTemplateList.Count > 0)
        {
            _todayQuestTemplateList.Clear();
        }

        Debug.Log($"(QUEST) Resetting new quest list...");
        for (int i = 0; i < _numberOfQuest; i++)
        {
            int randomIndex = Random.Range(0, _questTemplateList.Count - 1);

            Quest quest = (Quest)_questTemplateList[randomIndex].Clone();
            Quest existedQuest = _todayQuestTemplateList.Find(x => x.questType == quest.questType);

            if (_todayQuestTemplateList.Contains(quest) || existedQuest != null)
            {
                i -= 1;
                continue;
            }

            _todayQuestTemplateList.Add(quest);

            QuestBaseData questBaseData = new QuestBaseData();
            questBaseData.AddData(quest.questID, quest.currentProgress, quest.targetProgress, quest.isCompleted);
            AddBaseData(questBaseData);
        }

        EventManager.EmitEvent(EventID.QUEST_RESETTING);

        _initTime = DateTime.Today.Ticks;
        DateTime initDate = new DateTime(_initTime);
        Debug.Log($"--- (QUEST) INIT QUEST TIME --- DAY: {initDate}");
        DateTime nextResetDate = initDate + _resetDelayTimeSpan;
        Debug.Log($"--- (QUEST) NEXT RESET QUEST TIME --- DAY: {nextResetDate}");

        SaveTime();
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

    private bool IsNewDay()
    {
        long nextResetTime = _initTime + _resetDelayTimeSpan.Ticks;
        long todayTicks = DateTime.Today.Ticks;
        Debug.Log($"(QUEST) nextResetTimeTicks: {nextResetTime} --- todayTicks: {todayTicks}");

        if (todayTicks > nextResetTime) return true;

        return false;
    }
}