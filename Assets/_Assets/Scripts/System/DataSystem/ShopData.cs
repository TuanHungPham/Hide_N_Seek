using System;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public class ShopData : BaseData
{
    public int id;
    public bool isSelected;
    public bool isOwned;

    public void SetSelectionState(bool set)
    {
        isSelected = set;
        SetModified(true);
    }

    public void SetOwnedState(bool set)
    {
        isOwned = set;
        SetModified(true);
    }

    public string ToJsonString()
    {
        string jsonString = JsonConvert.SerializeObject(this);
        Debug.Log($"(DATA) SHOPDATA Json String: {jsonString}");
        return jsonString;
    }

    public void ParseToData(string json)
    {
        ShopData parsedShopData = JsonConvert.DeserializeObject<ShopData>(json);
        AddData(parsedShopData.id, parsedShopData.isSelected, parsedShopData.isOwned);
    }

    public void AddData(int id, bool isSelected, bool isOwned)
    {
        this.id = id;
        this.isSelected = isSelected;
        this.isOwned = isOwned;
    }
}