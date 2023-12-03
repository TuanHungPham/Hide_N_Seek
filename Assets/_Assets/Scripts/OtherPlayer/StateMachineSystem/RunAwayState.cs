using UnityEngine;

public class RunAwayState : MovingState
{
    public override void OnEnterState(StateMachineController stateMachineController)
    {
        LoadComponents(stateMachineController);
        SetupMovingType();
        _iMovingSystemAI.SetInitialDestination();
        base.OnEnterState(stateMachineController);

        // Debug.Log($"{currentAIPlayer.name} - Patrolling state.....");
    }

    public override void OnUpdateState(StateMachineController stateMachineController)
    {
        _aiController.GetInGameState().SetIsMakingFootstep(true);

        base.OnUpdateState(stateMachineController);
    }

    public override void OnCheckingState(StateMachineController stateMachineController)
    {
        if (_aiController.GetInGameState().IsCaught())
        {
            stateMachineController.SwitchState(stateMachineController.stationaryState);
        }
        else if (IsAnyHiderCaught() && !IsNearAnySeeker() && CanChangeState())
        {
            stateMachineController.SwitchState(stateMachineController.rescuingState);
        }
        else if (CanChangeState())
        {
            stateMachineController.SwitchState(stateMachineController.stationaryState);
        }
    }

    private void SetupMovingType()
    {
        _iMovingSystemAI = new HiderMovingSystem();

        _iMovingSystemAI.CurrentAIPlayer = currentAIPlayer;
        _iMovingSystemAI.AIController = _aiController;

        // Debug.Log($"{currentAIPlayer.name} - Seeker Moving System");
    }

    private bool CanChangeState()
    {
        return _iMovingSystemAI.CanChangeState();
    }

    private bool IsNearAnySeeker()
    {
        Vector3 seekerCurrentPos = GameplaySystem.Instance.GetNearestSeekerPosition(currentAIPlayer);
        var currentPosition = currentAIPlayer.position;

        float distanceToSeeker = Vector3.Distance(currentPosition, seekerCurrentPos);

        if (distanceToSeeker <= Distance.DISTANCE_TO_SEEKER) return true;

        return false;
    }

    private bool IsAnyHiderCaught()
    {
        int numberOfCaughtHider = GameplaySystem.GetNumberOfCaughtHider();
        if (numberOfCaughtHider > 0) return true;

        return false;
    }
}