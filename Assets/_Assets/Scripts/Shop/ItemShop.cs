using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class ItemShop
{
    [SerializeField] private int _itemID;
    [SerializeField] private int _itemPrice;
    [SerializeField] private Sprite _itemImage;

    [SerializeField] private bool _isOwned;
    [SerializeField] private bool _isSelected;

    public void SetOwnedState(bool set)
    {
        _isOwned = set;
    }

    public void SetSelectState(bool set)
    {
        _isSelected = set;
    }

    public bool IsSelected()
    {
        return _isSelected;
    }

    public bool IsOwned()
    {
        return _isOwned;
    }

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