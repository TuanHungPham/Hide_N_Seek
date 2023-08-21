using UnityEngine;

public class RescuingState : MovingState
{
    public override void OnEnterState(StateMachineController stateMachineController)
    {
        LoadComponents(stateMachineController);
        SetupMovingType();

        Debug.Log($"{currentAIPlayer.name} - Rescuing state.....");
    }

    public override void OnCheckingState(StateMachineController stateMachineController)
    {
        if (_aiController.GetInGameState().IsCaught())
        {
            stateMachineController.SwitchState(stateMachineController.stationaryState);
        }
        else if (IsNearAnySeeker() || !IsAnyHiderCaught())
        {
            stateMachineController.SwitchState(stateMachineController.runAwayState);
        }
    }

    private bool IsAnyHiderCaught()
    {
        foreach (var hider in GameplaySystem.Instance.GetHiderList())
        {
            if (hider == currentAIPlayer) continue;

            Controller hiderController = hider.GetComponent<Controller>();
            if (!hiderController.GetInGameState().IsCaught()) continue;

            return true;
        }

        return false;
    }

    private bool IsNearAnySeeker()
    {
        Vector3 seekerCurrentPos = GameplaySystem.Instance.GetNearestSeekerPosition(currentAIPlayer);
        var currentPosition = currentAIPlayer.position;

        float distanceToSeeker = Vector3.Distance(currentPosition, seekerCurrentPos);

        if (distanceToSeeker <= Distance.DISTANCE_TO_SEEKER) return true;

        return false;
    }

    private void SetupMovingType()
    {
        _iMovingSystemAI = new HiderRescuingSystem();

        _iMovingSystemAI.CurrentAIPlayer = currentAIPlayer;
        _iMovingSystemAI.AIController = _aiController;

        Debug.Log($"{currentAIPlayer.name} - Hider Rescuing System");
    }
}