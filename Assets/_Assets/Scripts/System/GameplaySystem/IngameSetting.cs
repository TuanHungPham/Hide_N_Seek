using System;
using System.Collections.Generic;
using UnityEngine;

public enum eSetting
{
    THEME_SOUND,
    SFX_SOUND,

    MAX_COUNT,
}

public class IngameSetting : PermanentMonoSingleton<IngameSetting>
{
    private Dictionary<eSetting, bool> _settingDict;

    private void Start()
    {
        InitSoundState();
    }

    private void InitSoundState()
    {
        _settingDict = new Dictionary<eSetting, bool>();

        int maxCount = (int)eSetting.MAX_COUNT;

        for (int i = 0; i < maxCount; i++)
        {
            eSetting setting = (eSetting)i;
            bool value = bool.Parse(PlayerPrefs.GetString(setting.ToString(), "True"));

            _settingDict.Add(setting, value);
        }
    }

    public void SetConfig(eSetting setting, bool set)
    {
        _settingDict[setting] = set;
    }

    public bool GetSettingState(eSetting setting)
    {
        return _settingDict[setting];
    }

    private void SaveSettingState()
    {
        foreach (var setting in _settingDict)
        {
            string key = setting.Key.ToString();
            string value = setting.Value.ToString();
            Debug.Log($"(SETTING) Saving: {key} --- {value}");

            PlayerPrefs.SetString(key, value);
        }
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SaveSettingState();
        }
    }

    private void OnApplicationQuit()
    {
        SaveSettingState();
    }
}