using UnityEngine;

public class PlayerController : Controller
{
    private static PlayerController instance;
    public static PlayerController Instance => instance;

    #region private

    [SerializeField] private MovingSystem _movingSystem;

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
    }
}