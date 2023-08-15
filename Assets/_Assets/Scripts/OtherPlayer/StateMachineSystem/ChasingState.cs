using UnityEngine;

public class ChasingState : MovingState
{
    public override void OnEnterState(StateMachineController stateMachineController)
    {
        LoadComponents(stateMachineController);
        SetupMovingType();

        Debug.Log($"{currentAIPlayer.name} - Chasing state.....");
    }

    public new void OnCheckingState(StateMachineController stateMachineController)
    {
        if (!_iMovingSystemAI.CanChangeState()) return;

        stateMachineController.SwitchState(stateMachineController.movingState);
    }

    private void SetupMovingType()
    {
        _iMovingSystemAI = new SeekerChasingSystem();

        _iMovingSystemAI.CurrentAIPlayer = currentAIPlayer;
        _iMovingSystemAI.AIController = _aiController;

        Debug.Log($"{currentAIPlayer.name} - Seeker Chasing System");
    }
}