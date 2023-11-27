using UnityEngine;

public class GameplayManager : TemporaryMonoSingleton<GameplayManager>
{
    [SerializeField] private ResourcesManager _resourcesManager;
    [SerializeField] private GameFlowManager _gameFlowManager;

    protected override void Awake()
    {
        base.Awake();
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

    public void AddCoin(long quantity)
    {
        _resourcesManager.AddTotalCoin(quantity);
    }

    public void ConsumeCoin(long quantity)
    {
        _resourcesManager.ConsumeCoin(quantity);
    }

    public void AddTicket(long quantity)
    {
        _resourcesManager.AddTicket(quantity);
    }

    public void ConsumeTicket(long quantity)
    {
        _resourcesManager.ConsumeTicket(quantity);
    }

    public long GetTotalCoin()
    {
        return _resourcesManager.GetTotalCoin();
    }

    public long GetTotalTicket()
    {
        return _resourcesManager.GetTotalTicket();
    }

    public long GetCoin(eAddingCoinType type)
    {
        return _resourcesManager.GetCoin(type);
    }
}