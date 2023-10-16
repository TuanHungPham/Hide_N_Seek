using System;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public abstract class BaseData
{
    [SerializeField] protected bool _isModified;

    public bool IsModified
    {
        get => _isModified;
        protected set => _isModified = value;
    }

    public string ToJsonString()
    {
        string jsonString = JsonConvert.SerializeObject(this);
        Debug.Log($"(DATA) Json String: {jsonString}");
        return jsonString;
    }

    public abstract void ParseToData(string json);

    public void SetModified(bool set)
    {
        IsModified = set;
    }
}