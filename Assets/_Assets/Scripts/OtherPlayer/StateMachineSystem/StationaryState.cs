using UnityEngine;

public class StationaryState : IState
{
    private IStantionarySystemAI _stantionarySystemAI;
    private AIController _aiController;
    private Transform currentAIPlayer;

    public void OnEnterState(StateMachineController stateMachineController)
    {
        LoadComponents(stateMachineController);
        SetSystemType();
        Debug.Log($"{currentAIPlayer.name} - IDLE state.....");
    }

    public void OnUpdateState(StateMachineController stateMachineController)
    {
        OnCheckingState(stateMachineController);
    }

    public void OnFixedUpdateState(StateMachineController stateMachineController)
    {
    }

    public void OnExitState(StateMachineController stateMachineController)
    {
    }

    public void OnCheckingState(StateMachineController stateMachineController)
    {
        if (!_stantionarySystemAI.CanChangeState()) return;

        if (_aiController.GetInGameState().IsSeeker())
        {
            stateMachineController.SwitchState(stateMachineController.patrollingState);
            return;
        }

        stateMachineController.SwitchState(stateMachineController.movingState);
    }

    private void LoadComponents(StateMachineController stateMachineController)
    {
        _aiController = stateMachineController.GetAIController();
        currentAIPlayer = stateMachineController.GetAIPlayer();
    }

    private void SetSystemType()
    {
        bool isSeeker = _aiController.GetInGameState().IsSeeker();

        if (isSeeker)
        {
            _stantionarySystemAI = new SeekerStantionarySystem();
            Debug.Log($"{currentAIPlayer.name} - Seeker Stantionary System");
        }
        else
        {
            _stantionarySystemAI = new HiderStantionarySystem();
            Debug.Log($"{currentAIPlayer.name} - Hider Stantionary System");
        }

        _stantionarySystemAI.CurrentAIPlayer = currentAIPlayer;
        _stantionarySystemAI.AIController = _aiController;
    }
}