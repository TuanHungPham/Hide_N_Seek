using System;
using UnityEngine;

public class StartingGameSystem : MonoBehaviour
{
    #region private

    [SerializeField] private SetupGameplayType _setupGameplayType;
    [SerializeField] private SetupStartingSpawn _setupStartingSpawn;

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
        _setupGameplayType = GetComponentInChildren<SetupGameplayType>();
        _setupStartingSpawn = GetComponentInChildren<SetupStartingSpawn>();
    }
}