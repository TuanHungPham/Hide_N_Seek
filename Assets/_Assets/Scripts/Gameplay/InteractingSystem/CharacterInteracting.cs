using UnityEngine;
using TigerForge;

public class CharacterInteracting : MonoBehaviour
{
    [SerializeField] private Controller _controller;
    [SerializeField] private LayerMask _layerMask;
    private InGameManager InGameManager => InGameManager.Instance;
    private GameplayManager GameplayManager => GameplayManager.Instance;

    public void CheckObjColliderState(GameObject target)
    {
        if (!CanInteract(target)) return;

        Controller targetController = target.GetComponent<Controller>();
        if (targetController == null) return;

        bool targetIsSeeker = targetController.GetInGameState().IsSeeker();
        bool thisObjIsSeeker = _controller.GetInGameState().IsSeeker();

        if (thisObjIsSeeker) return;

        CollideAsHider(targetIsSeeker, targetController);
    }

    private void CollideAsHider(bool targetIsSeeker, Controller targetController)
    {
        if (targetIsSeeker) return;

        if (!targetController.GetInGameState().IsCaught() || _controller.GetInGameState().IsCaught()) return;

        RescueTarget(targetController);
    }

    private void RescueTarget(Controller targetController)
    {
        if (_controller.GetPlayerType() == ePlayerType.MAIN_CHARACTER)
        {
            UpdateInGameAchievement(eAchievementType.RESCUING_TIME);
            AddBonusCoin();
        }

        targetController.SetCaughtState(false);
        EmitRescuingEvent();
    }

    private bool CanInteract(GameObject target)
    {
        if ((_layerMask.value & (1 << target.gameObject.layer)) == 0) return false;

        return true;
    }

    private void EmitRescuingEvent()
    {
        EventManager.EmitEvent(EventID.RESCUING_HIDER);
    }

    private void UpdateInGameAchievement(eAchievementType type)
    {
        InGameManager.AddAchievementPoint(type);
    }

    private void AddBonusCoin()
    {
        GameplayManager.AddCoin(eAddingCoinType.INTERACTING_BONUS_COIN, 10);
    }
}