using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    private static GameplayManager instance;
    public static GameplayManager Instance => instance;

    [SerializeField] private ResourcesManager _resourcesManager;
    [SerializeField] private GameFlowManager _gameFlowManager;

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
        _resourcesManager = GetComponentInChildren<ResourcesManager>();
        _gameFlowManager = GetComponentInChildren<GameFlowManager>();
    }

    public void AddCoin(eAddingCoinType type, long quantity)
    {
        _resourcesManager.AddCoin(type, quantity);
    }

    public void ConsumeCoin(long quantity)
    {
        _resourcesManager.ConsumeCoin(quantity);
    }

    public long GetTotalCoin()
    {
        return _resourcesManager.GetTotalCoin();
    }
}