using UnityEngine;
using UnityEngine.AI;

public abstract class MovingState : IState
{
    [Space(20)] protected AIController _aiController;
    protected NavMeshAgent _navMeshAgent;
    protected Vector3 destination;
    protected Transform currentAIPlayer;
    protected IMovingSystemAI _iMovingSystemAI;

    public virtual void OnEnterState(StateMachineController stateMachineController)
    {
        LoadComponents(stateMachineController);
        SetupMovingType();
        _iMovingSystemAI.SetInitialDestination();

        // Debug.Log($"{currentAIPlayer.name} - Moving state.....");
    }

    public virtual void OnUpdateState(StateMachineController stateMachineController)
    {
        GetDestination();
        OnCheckingState(stateMachineController);
    }

    public virtual void OnFixedUpdateState(StateMachineController stateMachineController)
    {
        Move(destination);
    }

    public virtual void OnExitState(StateMachineController stateMachineController)
    {
        _navMeshAgent.ResetPath();
    }

    public abstract void OnCheckingState(StateMachineController stateMachineController);

    public virtual void LoadComponents(StateMachineController stateMachineController)
    {
        _aiController = stateMachineController.GetAIController();
        _navMeshAgent = stateMachineController.GetNavMeshAgent();
        currentAIPlayer = stateMachineController.GetAIPlayer();
    }

    private void SetupMovingType()
    {
        _iMovingSystemAI = new HiderMovingSystem();

        _iMovingSystemAI.CurrentAIPlayer = currentAIPlayer;
        _iMovingSystemAI.AIController = _aiController;

        // Debug.Log($"{currentAIPlayer.name} - Hider Moving System");
    }

    public virtual void GetDestination()
    {
        _iMovingSystemAI.HandleGettingDestination();
        destination = _iMovingSystemAI.Destination;
    }

    public virtual void Move(Vector3 pos)
    {
        _navMeshAgent.SetDestination(pos);
    }
}