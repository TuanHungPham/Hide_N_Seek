using System.Collections.Generic;
using UnityEngine;

public class IngameDataManager : MonoBehaviour
{
    [SerializeField] private CostumeDataManager _costumeDataManager;
    [SerializeField] private PetDataManager _petDataManager;
    [SerializeField] private ResourceDataManager _resourceDataManager;
    [SerializeField] private QuestDataManager _questDataManager;
    [SerializeField] private AchievementDataManager _achievementDataManager;
    public static IngameDataManager Instance { get; private set; }

    private void Awake()
    {
        DontDestroyOnLoad(this);
        HandleSingleton();
        LoadComponents();
    }

    private void HandleSingleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
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

    public List<ItemShop> GetItemShopDataList(eShopDataType type)
    {
        switch (type)
        {
            case eShopDataType.PET_SHOP:
                return _petDataManager.GetItemShopList();
            case eShopDataType.COSTUME_SHOP:
                return _costumeDataManager.GetItemShopList();
            default:
                return null;
        }
    }

    public Costume GetCurrentUsingCostume()
    {
        return _costumeDataManager.GetCurrentUsingItem();
    }

    public GameObject GetCurrentUsingPet()
    {
        return _petDataManager.GetCurrentPet();
    }

    public Dictionary<eAchievementDataType, long> GetAchievementDataDic()
    {
        return _achievementDataManager.GetAchievementDataDic();
    }

    public void AddResourceData(eResourceDataType type, long quantity)
    {
        _resourceDataManager.AddData(type, quantity);
    }

    public void SetResourceData(eResourceDataType type, long quantity)
    {
        _resourceDataManager.SetData(type, quantity);
    }

    public long GetResourceData(eResourceDataType type)
    {
        return _resourceDataManager.GetResourceData(type);
    }

    public List<Quest> GetTodayQuestTemplateList()
    {
        return _questDataManager.GetTodayQuestTemplateList();
    }

    public SpecialQuest GetTodaySpecialQuest()
    {
        return _questDataManager.GetTodaySpecialQuest();
    }

    public QuestDataManager GetQuestDataManager()
    {
        return _questDataManager;
    }
}