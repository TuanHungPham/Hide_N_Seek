using System;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public class ShopData : BaseData
{
    public int id;
    public bool isSelected;
    public bool isOwned;

    public string ToJsonString()
    {
        string jsonString = JsonConvert.SerializeObject(this);
        Debug.Log($"(DATA) SHOPDATA Json String: {jsonString}");
        return jsonString;
    }

    public void AddData(int id, bool isSelected, bool isOwned)
    {
        this.id = id;
        this.isSelected = isSelected;
        this.isOwned = isOwned;
    }
}