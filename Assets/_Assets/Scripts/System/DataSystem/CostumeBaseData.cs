using System;

[Serializable]
public class CostumeBaseData : ShopData
{
    public void AddData(CostumeBaseData costumeBaseData)
    {
        id = costumeBaseData.id;
        isSelected = costumeBaseData.isSelected;
        isOwned = costumeBaseData.isOwned;
    }
}