using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/IAP/IAPProductTemplate")]
public class IAPProductTemplate : ScriptableObject
{
    [SerializeField] private List<IAPProduct> _iapProductTemplateList = new List<IAPProduct>();

    public List<IAPProduct> IAPProductTemplateList => _iapProductTemplateList;
}