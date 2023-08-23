using UnityEngine;

public class PlayerController : Controller
{
    private static PlayerController instance;
    public static PlayerController Instance => instance;

    #region private

    [SerializeField] private MovingSystem _movingSystem;
    [SerializeField] private BoosterUsingHandler _boosterUsingHandler;

    #endregion

    protected override void Awake()
    {
        instance = this;
        base.Awake();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        _movingSystem = GetComponentInChildren<MovingSystem>();
        _boosterUsingHandler = GetComponentInChildren<BoosterUsingHandler>();
    }

    public MovingSystem GetMovingSystem()
    {
        return _movingSystem;
    }

    public BoosterUsingHandler GetBoosterUsingHandler()
    {
        return _boosterUsingHandler;
    }

    public void SetCurrentUsingBooster(BoosterID boosterID)
    {
        _boosterUsingHandler.SetCurrentUsingBooster(boosterID);
    }
}