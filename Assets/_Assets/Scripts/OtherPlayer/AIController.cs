using UnityEngine;

public class AIController : Controller
{
    #region private

    [SerializeField] private AIManager _aiManager;

    #endregion

    protected override void LoadComponents()
    {
        base.LoadComponents();
        _aiManager = GetComponentInChildren<AIManager>();
    }
}