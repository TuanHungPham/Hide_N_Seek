using System;
using System.Collections.Generic;
using TigerForge;
using UnityEngine;
using UnityEngine.Serialization;

public enum eAddingCoinType
{
    NONE,
    PICK_UP_COIN,
    RESCUE_BONUS_COIN,

    MAX_NUMBER_OF_COIN_TYPE,
}

public class ResourcesManager : MonoBehaviour
{
    [SerializeField] private long _totalCoin;
    private Dictionary<eAddingCoinType, long> _coinTypeDic = new Dictionary<eAddingCoinType, long>();

    private void Start()
    {
        ListenEvent();
        InitializeCoinDictionary();
        InitializeCoin();
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.END_GAME, SetTotalCoin);
    }

    private void InitializeCoin()
    {
        _totalCoin = IngameDataManager.Instance.GetResourceData(eResourceDataType.COIN);
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

    private void SetTotalCoin()
    {
        foreach (KeyValuePair<eAddingCoinType, long> item in _coinTypeDic)
        {
            _totalCoin += item.Value;
        }

        IngameDataManager.Instance.SetResourceData(eResourceDataType.COIN, _totalCoin);
    }

    public long GetTotalCoin()
    {
        return _totalCoin;
    }

    public void AddCoin(eAddingCoinType type, long quantity)
    {
        if (!_coinTypeDic.ContainsKey(type)) return;

        _coinTypeDic[type] += quantity;
        LogSystem.LogDictionary(_coinTypeDic);
    }

    public void ConsumeCoin(long quantity)
    {
        _totalCoin -= quantity;
        SetTotalCoin();
    }
}