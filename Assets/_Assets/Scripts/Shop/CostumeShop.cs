using System;
using UnityEngine;

[Serializable]
public class CostumeShop
{
    [SerializeField] private int _costumeID;
    [SerializeField] private int _costumePrice;
    [SerializeField] private Sprite _costumeImage;

    public int GetCostumeID()
    {
        return _costumeID;
    }

    public int GetCostumePrice()
    {
        return _costumePrice;
    }

    public Sprite GetCostumeImage()
    {
        return _costumeImage;
    }
}