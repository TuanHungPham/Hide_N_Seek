using System;
using UnityEngine;

[Serializable]
public class Costume : ICloneable
{
    [SerializeField] private int _costumeID;
    [SerializeField] private Material _costumeMaterial;
    [SerializeField] private bool _isSelected;
    [SerializeField] private bool _isOwned;

    public Costume(int id, Material material, bool isSelected, bool isOwned)
    {
        _costumeID = id;
        _costumeMaterial = material;
        _isSelected = isSelected;
        _isOwned = isOwned;
    }

    public void SetSelected(bool set)
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

    public object Clone()
    {
        return new Costume(_costumeID, _costumeMaterial, _isSelected, _isOwned);
    }
}