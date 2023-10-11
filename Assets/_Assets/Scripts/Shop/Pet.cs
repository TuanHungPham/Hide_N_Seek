using System;
using UnityEngine;

[Serializable]
public class Pet : ICloneable
{
    [SerializeField] private int _petID;
    [SerializeField] private GameObject _petPrefab;
    [SerializeField] private bool _isSelected;
    [SerializeField] private bool _isOwned;

    public Pet(int id, GameObject petObj, bool isSelected, bool isOwned)
    {
        _petID = id;
        _petPrefab = petObj;
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

    public int GetPetID()
    {
        return _petID;
    }

    public GameObject GetPet()
    {
        return _petPrefab;
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
        return new Pet(_petID, _petPrefab, _isSelected, _isOwned);
    }
}