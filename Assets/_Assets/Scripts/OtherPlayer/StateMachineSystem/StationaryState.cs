using UnityEngine;

public class StationaryState : IState
{
    private IStantionarySystemAI _stantionarySystemAI;
    private AIController _aiController;
    private Transform currentAIPlayer;
    private GameplaySystem GameplaySystem => GameplaySystem.Instance;

    public void OnEnterState(StateMachineController stateMachineController)
    {
        LoadComponents(stateMachineController);
        SetSystemType();
        SetIdleAnimation();
        SetMovingAnimation(0);
        // Debug.Log($"{currentAIPlayer.name} - IDLE state.....");
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
        if (!CanChangeState()) return;

        if (_aiController.GetInGameState().IsSeeker())
        {
            if (GameplaySystem.Instance.IsInHidingTimer()) return;

            stateMachineController.SwitchState(stateMachineController.patrollingState);
            return;
        }


        if (CanComeToRescue())
        {
            stateMachineController.SwitchState(stateMachineController.rescuingState);
            return;
        }

        if (IsNearSeeker())
        {
            stateMachineController.SwitchState(stateMachineController.runAwayState);
        }
    }

    public bool CanChangeState()
    {
        return _stantionarySystemAI.CanChangeState();
    }

    private void LoadComponents(StateMachineController stateMachineController)
    {
        _aiController = stateMachineController.GetAIController();
        currentAIPlayer = stateMachineController.GetAIPlayer();
    }

    private void SetIdleAnimation()
    {
        _aiController.SetIdleAnimationState();
    }

    private void SetMovingAnimation(float value)
    {
        _aiController.SetMovingAnimation(value);
    }

    private void SetSystemType()
    {
        bool isSeeker = _aiController.GetInGameState().IsSeeker();

        if (isSeeker)
        {
            _stantionarySystemAI = new SeekerStantionarySystem();
            // Debug.Log($"{currentAIPlayer.name} - Seeker Stantionary System");
        }
        else
        {
            _stantionarySystemAI = new HiderStantionarySystem();
            // Debug.Log($"{currentAIPlayer.name} - Hider Stantionary System");
        }

        _stantionarySystemAI.CurrentAIPlayer = currentAIPlayer;
        _stantionarySystemAI.AIController = _aiController;

        _aiController.GetInGameState().SetIsMakingFootstep(false);
    }

    private bool CanComeToRescue()
    {
        if (!IsAnyHiderCaught()) return false;

        foreach (var caughtHider in GameplaySystem.GetHiderCaughtList())
        {
            Controller hiderController = caughtHider.GetComponent<Controller>();
            if (!hiderController.GetInGameState().IsCaught()) continue;

            var nearestSeeker = GameplaySystem.GetNearestSeekerPosition(caughtHider);
            var hiderPos = caughtHider.position;

            float distance = Vector3.Distance(hiderPos, nearestSeeker);

            if (distance <= Distance.DISTANCE_FROM_POINT_TO_SEEKER) continue;

            return true;
        }

        return false;
    }

    private bool IsAnyHiderCaught()
    {
        if (GameplaySystem.GetNumberOfCaughtHider() <= 0) return false;

        return true;
    }

    private bool IsNearSeeker()
    {
        Vector3 seekerCurrentPos = GameplaySystem.GetNearestSeekerPosition(currentAIPlayer);
        var currentPosition = currentAIPlayer.position;

        float distanceToSeeker = Vector3.Distance(currentPosition, seekerCurrentPos);

        if (distanceToSeeker <= Distance.DISTANCE_TO_SEEKER) return true;

        return false;
    }
}