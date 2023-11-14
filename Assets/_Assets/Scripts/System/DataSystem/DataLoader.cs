using System;
using System.Collections.Generic;
using TigerForge;
using UnityEngine;

public enum eDataType
{
    COSTUME_DATA,
    PET_DATA,
    QUEST_DATA,
    SPECIAL_QUEST_DATA,
    RESOURCE_DATA,
    ACHIEVEMENT_DATA,
}

public class DataLoader : MonoBehaviour
{
    [SerializeField] private CostumeDataManager _costumeDataManager;
    [SerializeField] private PetDataManager _petDataManager;
    [SerializeField] private ResourceDataManager _resourceDataManager;
    [SerializeField] private QuestDataManager _questDataManager;
    [SerializeField] private AchievementDataManager _achievementDataManager;

    private EasyFileSave myFile;

    private void Awake()
    {
        LoadComponents();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        _costumeDataManager = GetComponentInChildren<CostumeDataManager>();
        _petDataManager = GetComponentInChildren<PetDataManager>();
        _resourceDataManager = GetComponentInChildren<ResourceDataManager>();
        _questDataManager = GetComponentInChildren<QuestDataManager>();
        _achievementDataManager = GetComponentInChildren<AchievementDataManager>();

        myFile = new EasyFileSave();
    }

    private void Start()
    {
        LoadData();
    }

    private void SaveData()
    {
        // _achievementDataManager.SaveData();
        SaveCostumeData();
        SavePetData();
        SaveQuestData();
        SaveSpecialQuestData();
        SaveResourceData();
        SaveAchievementData();

        myFile.Append();
    }

    private void AddToSaveCache(eDataType type, object jsonStringList)
    {
        myFile.Add(type, jsonStringList);
    }

    private void LoadData()
    {
        if (!myFile.Load()) return;

        LoadCostumeData();
        LoadPetData();
        LoadQuestData();
        LoadSpecialQuestData();
        LoadResourceData();
        LoadAchievementData();

        myFile.Dispose();
    }

    private void SaveCostumeData()
    {
        List<string> jsonStringList = new List<string>();
        foreach (var baseData in _costumeDataManager.CostumeBaseDataList)
        {
            string jsonString = baseData.ToJsonString();
            jsonStringList.Add(jsonString);
        }

        AddToSaveCache(eDataType.COSTUME_DATA, jsonStringList);
    }

    private void SavePetData()
    {
        List<string> jsonStringList = new List<string>();
        foreach (var baseData in _petDataManager.PetBaseDataList)
        {
            string jsonString = baseData.ToJsonString();
            jsonStringList.Add(jsonString);
        }

        AddToSaveCache(eDataType.PET_DATA, jsonStringList);
    }

    private void SaveQuestData()
    {
        List<string> jsonStringList = new List<string>();
        foreach (var baseData in _questDataManager.QuestBaseDataList)
        {
            string jsonString = baseData.ToJsonString();
            jsonStringList.Add(jsonString);
        }

        AddToSaveCache(eDataType.QUEST_DATA, jsonStringList);
    }

    private void SaveSpecialQuestData()
    {
        List<string> jsonStringList = new List<string>();

        BaseData baseData = _questDataManager.SpecialQuestBaseData;
        string jsonString = baseData.ToJsonString();
        jsonStringList.Add(jsonString);

        AddToSaveCache(eDataType.SPECIAL_QUEST_DATA, jsonStringList);
    }

    private void SaveResourceData()
    {
        Dictionary<eResourceDataType, string> jsonStringDic = new Dictionary<eResourceDataType, string>();
        foreach (KeyValuePair<eResourceDataType, ResourceBaseData> baseData in _resourceDataManager.ResourcesBaseDataDic)
        {
            string jsonString = baseData.Value.ToJsonString();
            jsonStringDic.Add(baseData.Key, jsonString);
        }

        AddToSaveCache(eDataType.RESOURCE_DATA, jsonStringDic);
    }

    private void SaveAchievementData()
    {
        Dictionary<eAchievementType, string> jsonStringDic = new Dictionary<eAchievementType, string>();
        foreach (KeyValuePair<eAchievementType, AchievementBaseData> baseData in _achievementDataManager.AchievementBaseDataDic)
        {
            string jsonString = baseData.Value.ToJsonString();
            jsonStringDic.Add(baseData.Key, jsonString);
        }

        AddToSaveCache(eDataType.ACHIEVEMENT_DATA, jsonStringDic);
    }

    private void LoadCostumeData()
    {
        List<string> jsonStringList = GetJsonStringList(eDataType.COSTUME_DATA);
        if (jsonStringList == null) return;

        foreach (var json in jsonStringList)
        {
            CostumeBaseData loadedData = new CostumeBaseData();
            loadedData.ParseToData(json);

            _costumeDataManager.AddBaseData(loadedData);
        }

        string listName = "LOADING COSTUME DATA";
        LogSystem.LogList(jsonStringList, listName);
    }

    private void LoadPetData()
    {
        List<string> jsonStringList = GetJsonStringList(eDataType.PET_DATA);
        if (jsonStringList == null) return;

        foreach (var json in jsonStringList)
        {
            PetBaseData loadedData = new PetBaseData();
            loadedData.ParseToData(json);

            _petDataManager.AddBaseData(loadedData);
        }

        string listName = "LOADING PET DATA";
        LogSystem.LogList(jsonStringList, listName);
    }

    private void LoadQuestData()
    {
        List<string> jsonStringList = GetJsonStringList(eDataType.QUEST_DATA);
        if (jsonStringList == null) return;

        foreach (var json in jsonStringList)
        {
            QuestBaseData loadedData = new QuestBaseData();
            loadedData.ParseToData(json);

            _questDataManager.AddNormalQuestBaseData(loadedData);
        }

        string listName = "LOADING QUEST DATA";
        LogSystem.LogList(jsonStringList, listName);
    }

    private void LoadSpecialQuestData()
    {
        List<string> jsonStringList = GetJsonStringList(eDataType.SPECIAL_QUEST_DATA);
        if (jsonStringList == null) return;

        foreach (var json in jsonStringList)
        {
            QuestBaseData loadedData = new QuestBaseData();
            loadedData.ParseToData(json);

            _questDataManager.AddSpecialQuestBaseData(loadedData);
        }

        string listName = "LOADING SPECIAL QUEST DATA";
        LogSystem.LogList(jsonStringList, listName);
    }

    private void LoadResourceData()
    {
        Dictionary<eResourceDataType, string> jsonStringDic = (Dictionary<eResourceDataType, string>)myFile.GetData(eDataType.RESOURCE_DATA);
        if (jsonStringDic == null) return;

        foreach (var json in jsonStringDic)
        {
            ResourceBaseData resourceBaseData = new ResourceBaseData();
            resourceBaseData.ParseToData(json.Value);

            _resourceDataManager.AddBaseData(json.Key, resourceBaseData);
        }

        string dicName = "LOADING RESOURCE DATA";
        LogSystem.LogDictionary(jsonStringDic, dicName);
    }

    private void LoadAchievementData()
    {
        Dictionary<eAchievementType, string> jsonStringDic = (Dictionary<eAchievementType, string>)myFile.GetData(eDataType.ACHIEVEMENT_DATA);
        if (jsonStringDic == null) return;

        foreach (var json in jsonStringDic)
        {
            AchievementBaseData achievementBaseData = new AchievementBaseData();
            achievementBaseData.ParseToData(json.Value);

            _achievementDataManager.AddBaseData(json.Key, achievementBaseData);
        }

        string dicName = "LOADING ACHIEVEMENT DATA";
        LogSystem.LogDictionary(jsonStringDic, dicName);
    }

    private List<string> GetJsonStringList(eDataType type)
    {
        return (List<string>)myFile.GetData(type);
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SaveData();
        }
    }

    // private void OnApplicationQuit()
    // {
    //     SaveData();
    // }
}