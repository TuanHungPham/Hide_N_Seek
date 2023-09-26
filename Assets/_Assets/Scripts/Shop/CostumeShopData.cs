using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/CostumeShopList")]
public class CostumeShopData : ScriptableObject
{
    [SerializeField] private List<CostumeShop> _costumeShopList = new List<CostumeShop>();

    public List<CostumeShop> CostumeShopList
    {
        get => _costumeShopList;
        set => _costumeShopList = value;
    }
}