using System.Collections.Generic;
using UnityEngine;

public class GameplaySystem : MonoBehaviour
{
    private static GameplaySystem instance;

    public static GameplaySystem Instance => instance;

    #region private

    [SerializeField] private AllPlayerManager _allPlayerManager;

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
    }

    public AllPlayerManager GetAllPlayerManager()
    {
        return _allPlayerManager;
    }

    public List<Transform> GetAllPlayerList()
    {
        return _allPlayerManager.GetAllPlayerList();
    }
}