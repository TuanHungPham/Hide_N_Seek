using System;
using UnityEngine;

[Serializable]
public class Pet
{
    [SerializeField] private int _petID;
    [SerializeField] private GameObject _petPrefab;
    [SerializeField] private bool _isSelected;
    [SerializeField] private bool _isOwned;

    public void SetSelectedPet(bool set)
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
}