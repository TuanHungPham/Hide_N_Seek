using UnityEngine;

public class ChasingState : MovingState
{
    public override void OnEnterState(StateMachineController stateMachineController)
    {
        LoadComponents(stateMachineController);
        SetupMovingType();
        base.OnEnterState(stateMachineController);

        Debug.Log($"{currentAIPlayer.name} - Chasing state.....");
    }

    public override void OnCheckingState(StateMachineController stateMachineController)
    {
        if (!CanChangeState()) return;

        stateMachineController.SwitchState(stateMachineController.patrollingState);
    }

    private bool CanChangeState()
    {
        return _iMovingSystemAI.CanChangeState();
    }

    private void SetupMovingType()
    {
        _iMovingSystemAI = new SeekerChasingSystem();

        _iMovingSystemAI.CurrentAIPlayer = currentAIPlayer;
        _iMovingSystemAI.AIController = _aiController;

        Debug.Log($"{currentAIPlayer.name} - Seeker Chasing System");
    }
}