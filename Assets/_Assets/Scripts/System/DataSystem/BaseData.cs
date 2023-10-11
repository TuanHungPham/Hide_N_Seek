using System;
using UnityEngine;

[Serializable]
public class BaseData
{
    [SerializeField] protected bool _isModified;

    public bool IsModified
    {
        get => _isModified;
        protected set => _isModified = value;
    }

    public void SetModified(bool set)
    {
        IsModified = set;
    }
}