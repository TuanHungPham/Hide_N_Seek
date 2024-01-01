using System;

[Serializable]
public class PetBaseData : ShopData
{
    public void AddData(PetBaseData petBaseData)
    {
        id = petBaseData.id;
        isSelected = petBaseData.isSelected;
        isOwned = petBaseData.isOwned;
    }
}