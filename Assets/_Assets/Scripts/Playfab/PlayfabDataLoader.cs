using System.Collections.Generic;
using System.Text;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.Events;

public class PlayfabDataLoader : MonoBehaviour
{
    [SerializeField] private List<string> _dataKeyList = new List<string>();
    private Dictionary<string, UserDataRecord> _playfabUserData = new Dictionary<string, UserDataRecord>();
    private Dictionary<string, string> _dataCache = new Dictionary<string, string>();
    [SerializeField] private UnityEvent _loadingDataEvent;

    private void Start()
    {
        InitializeDataKey();
    }

    private void InitializeDataKey()
    {
        int maxCount = (int)eDataType.MAX_COUNT;

        for (int i = 0; i < maxCount; i++)
        {
            eDataType dataType = (eDataType)i;

            _dataKeyList.Add(dataType.ToString());
        }
    }

    public void AddDataToSaveCache(eDataType type, string data)
    {
        string dataType = type.ToString();

        _dataCache.Add(dataType, data);
    }

    public void SaveDataToServer()
    {
        UpdateUserDataRequest request = new UpdateUserDataRequest()
        {
            Data = _dataCache,
        };

        PlayFabClientAPI.UpdateUserData(request, OnUpdatingDataResult, OnErrorCallback);
    }

    public void LoadDataFromServer()
    {
        GetUserDataRequest request = new GetUserDataRequest()
        {
            Keys = _dataKeyList
        };

        PlayFabClientAPI.GetUserData(request, OnGettingDataResult, OnErrorCallback);
    }

    private void OnErrorCallback(PlayFabError error)
    {
        Debug.LogError($"(PLAYFAB) Playfab Error: {error.ErrorMessage}");
    }

    private void OnUpdatingDataResult(UpdateUserDataResult result)
    {
        Debug.Log($"(PLAYFAB) Playfab Update Data Result: {result}");
    }

    private void OnGettingDataResult(GetUserDataResult result)
    {
        _playfabUserData = result.Data;

        if (_playfabUserData == null) return;

        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine($"--- (PLAYFAB) USER DATA DICTIONARY LOGGING ---");
        foreach (KeyValuePair<string, UserDataRecord> item in _playfabUserData)
        {
            stringBuilder.AppendLine($"KEY: {item.Key} --- VALUE: {item.Value.Value}");
        }

        Debug.Log(stringBuilder);

        _loadingDataEvent?.Invoke();
    }

    public string GetUserData(eDataType type)
    {
        if (!_playfabUserData.ContainsKey(type.ToString())) return null;

        return _playfabUserData[type.ToString()].Value;
    }
}