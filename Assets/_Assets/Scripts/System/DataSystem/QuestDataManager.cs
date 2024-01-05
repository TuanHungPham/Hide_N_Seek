using System;
using System.Collections.Generic;
using TigerForge;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public enum eQuestDataType
{
    NORMAL_QUEST,
    SPECIAL_QUEST
}

public class QuestDataManager : MonoBehaviour
{
    [SerializeField] private List<QuestBaseData> _questBaseDataList = new List<QuestBaseData>();
    [SerializeField] private QuestBaseData _specialQuestBaseData;
    [SerializeField] private List<Quest> _todayQuestList = new List<Quest>();
    [SerializeField] private SpecialQuest _todaySpecialQuest;
    [SerializeField] private QuestTemplate _questTemplate;
    [SerializeField] private SpecialQuestTemplate _specialQuestTemplate;
    [SerializeField] private int _numberOfQuest;
    private List<Quest> _questTemplateList = new List<Quest>();
    private List<SpecialQuest> _specialQuestTemplateList = new List<SpecialQuest>();
    private long _initTime;
    private TimeSpan _resetDelayTimeSpan = TimeSpan.FromHours(24);

    public List<QuestBaseData> QuestBaseDataList => _questBaseDataList;
    public QuestBaseData SpecialQuestBaseData => _specialQuestBaseData;

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
        EventManager.StartListening(EventID.LOGIN_SUCCESS, ResetQuest);
    }

    private void ResetQuest()
    {
        QuestBaseDataList.Clear();
        _todayQuestList.Clear();
    }

    private void LoadTemplate()
    {
        LoadNormalQuestTemplateList();
        LoadSpecialQuestTemplateList();
    }

    private void LoadSpecialQuestTemplateList()
    {
        foreach (var questTemplate in _specialQuestTemplate.SpecialQuestList)
        {
            SpecialQuest quest = (SpecialQuest)questTemplate.Clone();

            _specialQuestTemplateList.Add(quest);
        }
    }

    private void LoadNormalQuestTemplateList()
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
            RandomDailyQuest();
            return;
        }

        Debug.Log($"(QUEST) Loading quest list...");

        LoadNormalQuestData();
        LoadSpecialQuestData();
    }

    private void LoadSpecialQuestData()
    {
        int specialQuestID = SpecialQuestBaseData.questID;
        float currentProgress = SpecialQuestBaseData.currentProgress;
        bool isCompleted = SpecialQuestBaseData.isCompleted;

        SpecialQuest specialQuest = _specialQuestTemplateList.Find(x => x.questID == specialQuestID);
        if (specialQuest == null) return;

        SpecialQuest loadedSpecialQuest = (SpecialQuest)specialQuest.Clone();

        loadedSpecialQuest.currentProgress = currentProgress;
        loadedSpecialQuest.isCompleted = isCompleted;

        _todaySpecialQuest = loadedSpecialQuest;
    }

    private void LoadNormalQuestData()
    {
        for (int i = 0; i < QuestBaseDataList.Count; i++)
        {
            int questID = QuestBaseDataList[i].questID;
            float currentProgress = QuestBaseDataList[i].currentProgress;
            bool isCompleted = QuestBaseDataList[i].isCompleted;

            Quest quest = _questTemplateList.Find(x => x.questID == questID);
            if (quest == null) return;

            Quest loadedQuest = (Quest)quest.Clone();

            loadedQuest.currentProgress = currentProgress;
            loadedQuest.isCompleted = isCompleted;

            _todayQuestList.Add(loadedQuest);
        }
    }

    public void RandomDailyQuest()
    {
        if (QuestBaseDataList.Count > 0)
        {
            QuestBaseDataList.Clear();
        }

        if (_todayQuestList.Count > 0)
        {
            _todayQuestList.Clear();
        }

        Debug.Log($"(QUEST) Resetting new quest list...");
        RandomNormalQuest();
        RandomSpecialQuest();

        EventManager.EmitEvent(EventID.QUEST_RESETTING);

        _initTime = DateTime.Today.Ticks;
        DateTime initDate = new DateTime(_initTime);
        Debug.Log($"--- (QUEST) INIT QUEST TIME --- DAY: {initDate}");
        DateTime nextResetDate = initDate + _resetDelayTimeSpan;
        Debug.Log($"--- (QUEST) NEXT RESET QUEST TIME --- DAY: {nextResetDate}");

        SaveTime();
    }

    private void RandomNormalQuest()
    {
        for (int i = 0; i < _numberOfQuest; i++)
        {
            int randomIndex = Random.Range(0, _questTemplateList.Count - 1);

            Quest quest = (Quest)_questTemplateList[randomIndex].Clone();
            Quest existedQuest = _todayQuestList.Find(x => x.questType == quest.questType);

            if (_todayQuestList.Contains(quest) || existedQuest != null)
            {
                i -= 1;
                continue;
            }

            _todayQuestList.Add(quest);

            QuestBaseData questBaseData = new QuestBaseData();
            questBaseData.AddData(quest.questID, quest.currentProgress, quest.targetProgress, quest.isCompleted);
            AddNormalQuestBaseData(questBaseData);
        }
    }

    private void RandomSpecialQuest()
    {
        int randomIndex = Random.Range(0, _specialQuestTemplateList.Count - 1);

        _todaySpecialQuest = (SpecialQuest)_specialQuestTemplateList[randomIndex].Clone();

        QuestBaseData questBaseData = new QuestBaseData();
        questBaseData.AddData(_todaySpecialQuest.questID, _todaySpecialQuest.currentProgress, _todaySpecialQuest.targetProgress, _todaySpecialQuest.isCompleted);
        AddSpecialQuestBaseData(questBaseData);
    }

    public void AddNormalQuestBaseData(QuestBaseData questBaseData)
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

    public void AddSpecialQuestBaseData(QuestBaseData questBaseData)
    {
        SpecialQuestBaseData.AddData(questBaseData);
    }

    private void UpdateBaseData()
    {
        for (int i = 0; i < _todayQuestList.Count; i++)
        {
            Quest quest = _todayQuestList[i];
            QuestBaseData questBaseData = _questBaseDataList[i];

            questBaseData.ModifyData(quest.currentProgress, quest.targetProgress, quest.isCompleted);
        }

        if (_todaySpecialQuest.currentProgress == SpecialQuestBaseData.currentProgress) return;

        SpecialQuestBaseData.ModifyData(_todaySpecialQuest.currentProgress, _todaySpecialQuest.targetProgress, _todaySpecialQuest.isCompleted);
    }

    public List<Quest> GetTodayQuestList()
    {
        return _todayQuestList;
    }

    public SpecialQuest GetTodaySpecialQuest()
    {
        return _todaySpecialQuest;
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