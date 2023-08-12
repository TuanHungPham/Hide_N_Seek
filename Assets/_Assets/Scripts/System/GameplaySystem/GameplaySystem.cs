using System.Collections.Generic;
using UnityEngine;

public class GameplaySystem : MonoBehaviour
{
    private static GameplaySystem instance;

    public static GameplaySystem Instance => instance;

    #region private

    [SerializeField] private AllPlayerManager _allPlayerManager;
    [SerializeField] private StartingGameSystem _spawningPlayer;

    #endregion

    private void Awake()
    {
        instance = this;
        LoadComponents();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        _allPlayerManager = GetComponentInChildren<AllPlayerManager>();
        _spawningPlayer = GetComponentInChildren<StartingGameSystem>();
    }

    public AllPlayerManager GetAllPlayerManager()
    {
        return _allPlayerManager;
    }

    public StartingGameSystem GetSpawningPlayer()
    {
        return _spawningPlayer;
    }

    public List<Transform> GetAllPlayerList()
    {
        return _allPlayerManager.GetAllPlayerList();
    }

    public Vector3 GetCurrentSeekerPosition()
    {
        return _allPlayerManager.GetCurrentSeekerPosition();
    }
}