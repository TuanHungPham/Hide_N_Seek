using UnityEngine;

public class PatrollingState : MovingState
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
        if (_aiController.GetInGameState().IsTriggered())
        {
            stateMachineController.SwitchState(stateMachineController.chasingState);
        }
        else if (_aiController.GetInGameState().IsHearingSomething())
        {
            stateMachineController.SwitchState(stateMachineController.hearingState);
        }
    }

    private void SetupMovingType()
    {
        _iMovingSystemAI = new SeekerPatrollingSystem();

        _iMovingSystemAI.CurrentAIPlayer = currentAIPlayer;
        _iMovingSystemAI.AIController = _aiController;

        // Debug.Log($"{currentAIPlayer.name} - Seeker Moving System");
    }
}