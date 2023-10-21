using System.Collections.Generic;
using TigerForge;
using UnityEngine;

public enum eDataType
{
    COSTUME_DATA,
    PET_DATA,
    QUEST_DATA,
    SPECIAL_QUEST_DATA,
}

public class DataLoader : MonoBehaviour
{
    [SerializeField] private CostumeDataManager _costumeDataManager;
    [SerializeField] private PetDataManager _petDataManager;
    [SerializeField] private ResourceDataManager _resourceDataManager;
    [SerializeField] private QuestDataManager _questDataManager;

    private EasyFileSave myFile;

    private void Awake()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        _costumeDataManager = GetComponentInChildren<CostumeDataManager>();
        _petDataManager = GetComponentInChildren<PetDataManager>();
        _resourceDataManager = GetComponentInChildren<ResourceDataManager>();
        _questDataManager = GetComponentInChildren<QuestDataManager>();

        myFile = new EasyFileSave();
    }

    private void Start()
    {
        LoadData();
    }

    private void SaveData()
    {
        _resourceDataManager.SaveData();
        SaveCostumeData();
        SavePetData();
        SaveQuestData();
        SaveSpecialQuestData();

        myFile.Append();
    }

    private void AddToSaveCache(eDataType type, List<string> jsonStringList)
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

    private List<string> GetJsonStringList(eDataType type)
    {
        return (List<string>)myFile.GetData(type);
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }
}