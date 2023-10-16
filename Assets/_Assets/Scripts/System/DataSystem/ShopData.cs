using System;
using Newtonsoft.Json;

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

    public override void ParseToData(string json)
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