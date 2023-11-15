using System;
using System.Collections.Generic;
using TigerForge;
using UnityEngine;
using UnityEngine.Serialization;

public enum eAddingCoinType
{
    NONE,
    PICK_UP_COIN,
    INTERACTING_BONUS_COIN,

    MAX_NUMBER_OF_COIN_TYPE,
}

public class ResourcesManager : MonoBehaviour
{
    [SerializeField] private long _totalCoin;
    [SerializeField] private long _totalTicket;
    private Dictionary<eAddingCoinType, long> _coinTypeDic = new Dictionary<eAddingCoinType, long>();
    private IngameDataManager IngameDataManager => IngameDataManager.Instance;

    private void Awake()
    {
        InitializeCoinDictionary();
        InitializeResources();
    }

    private void InitializeResources()
    {
        _totalCoin = IngameDataManager.Instance.GetResourceData(eResourceDataType.COIN);
        _totalTicket = IngameDataManager.Instance.GetResourceData(eResourceDataType.ADS_TICKET);
    }

    private void InitializeCoinDictionary()
    {
        int maxCount = (int)eAddingCoinType.MAX_NUMBER_OF_COIN_TYPE;

        for (int i = 1; i < maxCount; i++)
        {
            eAddingCoinType coinType = (eAddingCoinType)i;
            long coinQuantity = 0;

            _coinTypeDic.Add(coinType, coinQuantity);
        }
    }

    public long GetTotalCoin()
    {
        return _totalCoin;
    }

    public long GetTotalTicket()
    {
        return _totalTicket;
    }

    public void AddTotalCoin(long quantity)
    {
        _totalCoin += quantity;
        SetTotalCoin();
        EmitAddingResourcesEvent();
    }

    public void AddTicket(long quantity)
    {
        _totalTicket += quantity;
        SetTotalTicket();
        EmitAddingResourcesEvent();
    }

    public void ConsumeTicket(long quantity)
    {
        _totalTicket -= quantity;
        EmitConsumingResourcesEvent();
    }

    public void AddCoin(eAddingCoinType type, long quantity)
    {
        if (!_coinTypeDic.ContainsKey(type)) return;

        _coinTypeDic[type] += quantity;
        LogSystem.LogDictionary(_coinTypeDic);

        AddTotalCoin(quantity);

        EmitAddingResourcesEvent();
    }

    public void ConsumeCoin(long quantity)
    {
        _totalCoin -= quantity;
        SetTotalCoin();

        EmitConsumingResourcesEvent();
    }

    public long GetCoin(eAddingCoinType type)
    {
        return _coinTypeDic[type];
    }

    private void SetTotalTicket()
    {
        IngameDataManager.SetResourceData(eResourceDataType.ADS_TICKET, _totalTicket);
    }

    private void SetTotalCoin()
    {
        IngameDataManager.SetResourceData(eResourceDataType.COIN, _totalCoin);
    }

    private void EmitAddingResourcesEvent()
    {
        EventManager.EmitEvent(EventID.ADDING_RESOURCES);
    }

    private void EmitConsumingResourcesEvent()
    {
        EventManager.EmitEvent(EventID.CONSUMING_RESOURCES);
    }
}