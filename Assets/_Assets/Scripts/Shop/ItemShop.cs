using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class ItemShop
{
    [SerializeField] private int _itemID;
    [SerializeField] private int _itemPrice;
    [SerializeField] private Sprite _itemImage;

    public int GetItemID()
    {
        return _itemID;
    }

    public int GetItemPrice()
    {
        return _itemPrice;
    }

    public Sprite GetItemImage()
    {
        return _itemImage;
    }
}