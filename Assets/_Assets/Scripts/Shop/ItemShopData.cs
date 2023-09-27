using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Data/ItemShopData")]
public class ItemShopData : ScriptableObject
{
    [SerializeField] private List<ItemShop> _itemShopList = new List<ItemShop>();

    public List<ItemShop> ItemShopList
    {
        get => _itemShopList;
        set => _itemShopList = value;
    }
}