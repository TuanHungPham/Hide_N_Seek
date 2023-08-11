using UnityEngine;

public class AIController : Controller
{
    #region private

    [SerializeField] private AIMovingManager _aiMovingManager;

    #endregion

    protected override void LoadComponents()
    {
        base.LoadComponents();
        _aiMovingManager = GetComponentInChildren<AIMovingManager>();
    }
}