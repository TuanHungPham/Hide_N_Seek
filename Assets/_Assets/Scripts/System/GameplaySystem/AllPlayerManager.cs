using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AllPlayerManager : MonoBehaviour
{
    #region private

    [SerializeField] private Transform allPlayerParent;
    [SerializeField] private List<Transform> allPlayerList;

    #endregion

    private void Awake()
    {
        LoadComponents();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
    }

    private void Start()
    {
        InitializePlayerList();
    }

    private void InitializePlayerList()
    {
        foreach (Transform child in allPlayerParent)
        {
            if (allPlayerList.Contains(child)) continue;

            allPlayerList.Add(child);
        }
    }

    public List<Transform> GetAllPlayerList()
    {
        return allPlayerList;
    }
}