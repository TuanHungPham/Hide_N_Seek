using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/CostumeList")]
public class CostumeData : ScriptableObject
{
    [SerializeField] private List<Costume> _costumeDataList = new List<Costume>();

    public List<Costume> CostumeDataList
    {
        get => _costumeDataList;
        set => _costumeDataList = value;
    }
}