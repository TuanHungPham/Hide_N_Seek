using UnityEngine;

public class FindingState : MovingState
{
    public override void OnEnterState(StateMachineController stateMachineController)
    {
        LoadComponents(stateMachineController);
        SetupMovingType();
        base.OnEnterState(stateMachineController);
        _iMovingSystemAI.SetInitialDestination();

        // Debug.Log($"{currentAIPlayer.name} - Patrolling state.....");
    }

    public override void OnCheckingState(StateMachineController stateMachineController)
    {
        if (!_aiController.GetInGameState().IsHearingSomething())
        {
            stateMachineController.SwitchState(stateMachineController.patrollingState);
        }
    }

    private void SetupMovingType()
    {
        _iMovingSystemAI = new SeekerFindingSystem();

        _iMovingSystemAI.CurrentAIPlayer = currentAIPlayer;
        _iMovingSystemAI.AIController = _aiController;

        // Debug.Log($"{currentAIPlayer.name} - Seeker Moving System");
    }

    private bool IsNearToPointDestination()
    {
        float distance = Vector3.Distance(currentAIPlayer.position, _iMovingSystemAI.Destination);
        if (distance < Distance.DISTANCE_TO_CURRENT_POINT) return true;

        return false;
    }
}