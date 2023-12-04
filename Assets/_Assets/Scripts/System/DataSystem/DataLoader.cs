using System;
using System.Collections.Generic;
using Newtonsoft.Json;
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

    MAX_COUNT,
}

public class DataLoader : MonoBehaviour
{
    [SerializeField] private CostumeDataManager _costumeDataManager;
    [SerializeField] private PetDataManager _petDataManager;
    [SerializeField] private ResourceDataManager _resourceDataManager;
    [SerializeField] private QuestDataManager _questDataManager;
    [SerializeField] private AchievementDataManager _achievementDataManager;

    private PlayfabManager PlayfabManager => PlayfabManager.Instance;
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
    }

    private void Start()
    {
        Load();
    }

    private void Save()
    {
        SaveCostumeData();
        SavePetData();
        SaveQuestData();
        SaveSpecialQuestData();
        SaveResourceData();
        SaveAchievementData();

        if (PlayfabManager == null)
        {
            Debug.Log($"(DATA) Playfab nullllllllll!");
        }

        if (PlayfabManager != null && PlayfabManager.IsClientLoggedIn())
        {
            Debug.Log($"(DATA) Saving on Playfab Server");
            PlayfabManager.SaveDataToServer();
            return;
        }

        Debug.Log($"(DATA) Saving on local storage");
        myFile.Append();
    }

    private void AddDataToSaveCache(eDataType type, string data)
    {
        if (PlayfabManager == null)
        {
            Debug.Log($"(DATA) Playfab nullllllllll!");
        }

        if (PlayfabManager != null && PlayfabManager.IsClientLoggedIn())
        {
            PlayfabManager.AddDataToSaveCache(type, data);
            return;
        }

        Debug.Log($"(DATA) Local storage add {type}");
        myFile.Add(type, data);
    }

    private void Load()
    {
        myFile = new EasyFileSave();

        if (!myFile.Load()) return;

        LoadData();

        Debug.Log($"(DATA) Loading on local storage");
        myFile.Dispose();
    }

    public void LoadData()
    {
        LoadCostumeData();
        LoadPetData();
        LoadQuestData();
        LoadSpecialQuestData();
        LoadResourceData();
        LoadAchievementData();

        if (PlayfabManager.IsClientLoggedIn())
        {
            _petDataManager.LoadData();
            _costumeDataManager.LoadData();
            _achievementDataManager.LoadData();
            _questDataManager.LoadData();
            _resourceDataManager.LoadData();

            EmitLoadingServerDataEvent();
        }
    }

    private string SerializeDataToJson(object data)
    {
        return JsonConvert.SerializeObject(data);
    }

    private void SaveCostumeData()
    {
        List<string> jsonStringList = new List<string>();
        foreach (var baseData in _costumeDataManager.CostumeBaseDataList)
        {
            string jsonString = baseData.ToJsonString();
            jsonStringList.Add(jsonString);
        }

        string jsonData = SerializeDataToJson(jsonStringList);

        AddDataToSaveCache(eDataType.COSTUME_DATA, jsonData);
    }

    private void SavePetData()
    {
        List<string> jsonStringList = new List<string>();
        foreach (var baseData in _petDataManager.PetBaseDataList)
        {
            string jsonString = baseData.ToJsonString();
            jsonStringList.Add(jsonString);
        }

        string jsonData = SerializeDataToJson(jsonStringList);

        AddDataToSaveCache(eDataType.PET_DATA, jsonData);
    }

    private void SaveQuestData()
    {
        List<string> jsonStringList = new List<string>();
        foreach (var baseData in _questDataManager.QuestBaseDataList)
        {
            string jsonString = baseData.ToJsonString();
            jsonStringList.Add(jsonString);
        }

        string jsonData = SerializeDataToJson(jsonStringList);

        AddDataToSaveCache(eDataType.QUEST_DATA, jsonData);
    }

    private void SaveSpecialQuestData()
    {
        List<string> jsonStringList = new List<string>();

        BaseData baseData = _questDataManager.SpecialQuestBaseData;
        string jsonString = baseData.ToJsonString();
        jsonStringList.Add(jsonString);

        string jsonData = SerializeDataToJson(jsonStringList);

        AddDataToSaveCache(eDataType.SPECIAL_QUEST_DATA, jsonData);
    }

    private void SaveResourceData()
    {
        Dictionary<eResourceDataType, string> jsonStringDic = new Dictionary<eResourceDataType, string>();
        foreach (KeyValuePair<eResourceDataType, ResourceBaseData> baseData in _resourceDataManager.ResourcesBaseDataDic)
        {
            string jsonString = baseData.Value.ToJsonString();
            jsonStringDic.Add(baseData.Key, jsonString);
        }

        string jsonData = SerializeDataToJson(jsonStringDic);

        AddDataToSaveCache(eDataType.RESOURCE_DATA, jsonData);
    }

    private void SaveAchievementData()
    {
        Dictionary<eAchievementType, string> jsonStringDic = new Dictionary<eAchievementType, string>();
        foreach (KeyValuePair<eAchievementType, AchievementBaseData> baseData in _achievementDataManager.AchievementBaseDataDic)
        {
            string jsonString = baseData.Value.ToJsonString();
            jsonStringDic.Add(baseData.Key, jsonString);
        }

        string jsonData = SerializeDataToJson(jsonStringDic);

        AddDataToSaveCache(eDataType.ACHIEVEMENT_DATA, jsonData);
    }

    private T DeserializeJsonToData<T>(eDataType dataType)
    {
        string jsonData;
        if (PlayfabManager.IsClientLoggedIn())
        {
            jsonData = PlayfabManager.GetUserData(dataType);
        }
        else
        {
            jsonData = myFile.GetString(dataType);
        }

        if (string.IsNullOrEmpty(jsonData)) return default;

        var data = JsonConvert.DeserializeObject<T>(jsonData);
        return data;
    }

    private void LoadCostumeData()
    {
        List<string> jsonStringList = DeserializeJsonToData<List<string>>(eDataType.COSTUME_DATA);
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
        List<string> jsonStringList = DeserializeJsonToData<List<string>>(eDataType.PET_DATA);

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
        List<string> jsonStringList = DeserializeJsonToData<List<string>>(eDataType.QUEST_DATA);

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
        List<string> jsonStringList = DeserializeJsonToData<List<string>>(eDataType.SPECIAL_QUEST_DATA);

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
        Dictionary<eResourceDataType, string> jsonStringDic = DeserializeJsonToData<Dictionary<eResourceDataType, string>>(eDataType.RESOURCE_DATA);

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
        Dictionary<eAchievementType, string> jsonStringDic = DeserializeJsonToData<Dictionary<eAchievementType, string>>(eDataType.ACHIEVEMENT_DATA);

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

    private void EmitLoadingServerDataEvent()
    {
        EventManager.EmitEvent(EventID.LOADING_SERVER_DATA);
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            Save();
        }
    }

    private void OnApplicationQuit()
    {
        Save();
    }
}