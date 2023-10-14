using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class LogSystem
{
    public static void LogDictionary<T1, T2>(Dictionary<T1, T2> dictionary, string dicName = "")
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine($"--- {dicName} DICTIONARY LOGGING ---");
        foreach (KeyValuePair<T1, T2> item in dictionary)
        {
            stringBuilder.AppendLine($"KEY: {item.Key} --- VALUE: {item.Value}");
        }

        Debug.Log(stringBuilder);
    }

    public static void LogList<T>(List<T> list, string listName = "")
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine($"--- {listName} LIST LOGGING ---");

        for (int i = 0; i < list.Count; i++)
        {
            stringBuilder.AppendLine($" [{i}]: {list[i]}");
        }

        Debug.Log(stringBuilder);
    }
}