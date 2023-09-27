using System;
using UnityEngine;

[Serializable]
public class Costume
{
    [SerializeField] private int _costumeID;
    [SerializeField] private Material _costumeMaterial;
    [SerializeField] private bool _isSelected;
    [SerializeField] private bool _isOwned;

    public void SetSelectedCostume(bool set)
    {
        _isSelected = set;
    }

    public void SetOwned(bool set)
    {
        _isOwned = set;
    }

    public int GetCostumeID()
    {
        return _costumeID;
    }

    public Material GetCostume()
    {
        return _costumeMaterial;
    }

    public bool IsOwned()
    {
        return _isOwned;
    }

    public bool IsSelected()
    {
        return _isSelected;
    }
}