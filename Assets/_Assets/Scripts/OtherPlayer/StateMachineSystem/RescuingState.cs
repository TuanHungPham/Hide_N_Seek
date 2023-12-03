using UnityEngine;

public class RescuingState : MovingState
{
    public override void OnEnterState(StateMachineController stateMachineController)
    {
        LoadComponents(stateMachineController);
        SetupMovingType();
        base.OnEnterState(stateMachineController);

        Debug.Log($"{currentAIPlayer.name} - Rescuing state.....");
    }

    public override void OnCheckingState(StateMachineController stateMachineController)
    {
        if (_aiController.GetInGameState().IsCaught() || !CanComeToRescue())
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
        int numberOfCaughtHider = GameplaySystem.GetNumberOfCaughtHider();
        if (numberOfCaughtHider > 0) return true;

        return false;
    }

    private bool IsNearAnySeeker()
    {
        Vector3 seekerCurrentPos = GameplaySystem.GetNearestSeekerPosition(currentAIPlayer);
        var currentPosition = currentAIPlayer.position;

        float distanceToSeeker = Vector3.Distance(currentPosition, seekerCurrentPos);

        if (distanceToSeeker <= Distance.DISTANCE_TO_SEEKER) return true;

        return false;
    }

    private bool CanComeToRescue()
    {
        foreach (var caughtHider in GameplaySystem.GetHiderCaughtList())
        {
            Controller hiderController = caughtHider.GetComponent<Controller>();
            if (!hiderController.GetInGameState().IsCaught()) continue;

            var nearestSeeker = GameplaySystem.GetNearestSeekerPosition(caughtHider);
            var hiderPos = caughtHider.position;
            // var currentPos = currentAIPlayer.position;
            //
            // Vector3 dirToHider = (hiderPos - currentPos).normalized;
            // Vector3 dirToSeeker = (nearestSeeker - currentPos).normalized;
            //
            // float angle = Vector3.Angle(dirToHider, dirToSeeker);
            float distance = Vector3.Distance(hiderPos, nearestSeeker);

            if (distance <= Distance.DISTANCE_FROM_POINT_TO_SEEKER) continue;

            return true;
        }

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