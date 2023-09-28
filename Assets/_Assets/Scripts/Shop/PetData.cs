using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/ShopData/PetList")]
public class PetData : ScriptableObject
{
    [SerializeField] private List<Pet> _petDataList = new List<Pet>();

    public List<Pet> PetDataList
    {
        get => _petDataList;
        set => _petDataList = value;
    }
}