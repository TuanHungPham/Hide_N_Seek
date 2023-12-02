using UnityEngine;
using UnityEngine.AI;

public abstract class MovingState : IState
{
    protected AIController _aiController;
    protected NavMeshAgent _navMeshAgent;
    protected Vector3 destination;
    protected Transform currentAIPlayer;
    protected IMovingSystemAI _iMovingSystemAI;
    protected GameplaySystem GameplaySystem => GameplaySystem.Instance;

    public virtual void OnEnterState(StateMachineController stateMachineController)
    {
        SetMovingAnimation(1);
        _aiController.GetInGameState().SetIsMakingFootstep(true);
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

    public void LoadComponents(StateMachineController stateMachineController)
    {
        _aiController = stateMachineController.GetAIController();
        _navMeshAgent = stateMachineController.GetNavMeshAgent();
        currentAIPlayer = stateMachineController.GetAIPlayer();
    }

    public void GetDestination()
    {
        _iMovingSystemAI.HandleGettingDestination();
        destination = _iMovingSystemAI.Destination;
    }

    public void Move(Vector3 pos)
    {
        _navMeshAgent.SetDestination(pos);
    }

    public void SetMovingAnimation(float value)
    {
        _aiController.SetMovingAnimation(value);
    }
}