using System.Collections.Generic;
using UnityEngine;

public class IngameDataManager : PermanentMonoSingleton<IngameDataManager>
{
    [SerializeField] private CostumeDataManager _costumeDataManager;
    [SerializeField] private PetDataManager _petDataManager;
    [SerializeField] private ResourceDataManager _resourceDataManager;
    [SerializeField] private QuestDataManager _questDataManager;
    [SerializeField] private AchievementDataManager _achievementDataManager;
    [SerializeField] private int frameRate;

    protected override void Awake()
    {
        base.Awake();

        Application.targetFrameRate = frameRate;
        LoadComponents();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void Update()
    {
        if (Application.targetFrameRate != frameRate) Application.targetFrameRate = frameRate;
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

    public ItemShop GetCurrentSelectedCostumeItemShop()
    {
        return _costumeDataManager.GetCurrentSelectedCostumeItemShop();
    }

    public GameObject GetCurrentUsingPet()
    {
        return _petDataManager.GetCurrentPet();
    }

    public Dictionary<eAchievementType, long> GetAchievementDataDic()
    {
        return _achievementDataManager.GetAchievementDataDic();
    }

    public void AddResourceData(eResourceDataType type, long quantity)
    {
        _resourceDataManager.AddData(type, quantity);
    }

    public void AddAchievementData(eAchievementType type)
    {
        _achievementDataManager.AddData(type);
    }

    public void SetResourceData(eResourceDataType type, long quantity)
    {
        _resourceDataManager.SetData(type, quantity);
    }

    public long GetResourceData(eResourceDataType type)
    {
        return _resourceDataManager.GetResourceData(type);
    }

    public long GetAchievementData(eAchievementType type)
    {
        return _achievementDataManager.GetAchievementData(type);
    }

    public List<Quest> GetTodayQuestTemplateList()
    {
        return _questDataManager.GetTodayQuestList();
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