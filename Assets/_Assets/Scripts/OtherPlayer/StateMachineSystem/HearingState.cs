using UnityEngine;

public class HearingState : IState
{
    private float hearingTime = 1.5f;
    private float hearingTimer;
    private bool isHearing;

    private Transform currentAIPlayer;
    private AIController _aiController;

    public void OnEnterState(StateMachineController stateMachineController)
    {
        LoadComponents(stateMachineController);
    }

    public void OnUpdateState(StateMachineController stateMachineController)
    {
        SetIdleAnimation();
        RunHearingTimer();
        FaceAtFootstepDirection();
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
        if (isHearing) return;

        stateMachineController.SwitchState(stateMachineController.findingState);
    }

    private void LoadComponents(StateMachineController stateMachineController)
    {
        _aiController = stateMachineController.GetAIController();
        currentAIPlayer = stateMachineController.GetAIPlayer();

        hearingTimer = hearingTime;
        isHearing = true;
    }

    private void SetIdleAnimation()
    {
        _aiController.SetIdleAnimationState();
    }

    private void FaceAtFootstepDirection()
    {
        Transform footstepSoundMaking = _aiController.GetTriggeredSystem().GetFootstepSoundMaking();
        Vector3 directionToSoundMaking = (footstepSoundMaking.position - currentAIPlayer.position).normalized;

        Quaternion rotatation = Quaternion.LookRotation(directionToSoundMaking, Vector3.up);

        currentAIPlayer.rotation = Quaternion.RotateTowards(currentAIPlayer.rotation, rotatation, 1f);
    }

    private void RunHearingTimer()
    {
        if (hearingTimer > 0)
        {
            isHearing = true;
            hearingTimer -= Time.deltaTime;
            return;
        }

        isHearing = false;
    }
}