using System;
using UnityEngine;

[Serializable]
public class Costume
{
    [SerializeField] private int _costumeID;
    [SerializeField] private Material _costumeMaterial;
    [SerializeField] private bool _isOwned;

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
}