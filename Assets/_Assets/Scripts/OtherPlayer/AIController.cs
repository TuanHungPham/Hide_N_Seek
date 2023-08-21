using UnityEngine;

public class AIController : Controller
{
    #region private

    [SerializeField] private AIManager _aiManager;
    [SerializeField] private TriggeredSystem _triggeredSystem;

    #endregion

    protected override void LoadComponents()
    {
        base.LoadComponents();
        _aiManager = GetComponentInChildren<AIManager>();
        _triggeredSystem = GetComponentInChildren<TriggeredSystem>();
    }

    public TriggeredSystem GetTriggeredSystem()
    {
        return _triggeredSystem;
    }
}