public class FindingState : MovingState
{
    public override void OnEnterState(StateMachineController stateMachineController)
    {
        LoadComponents(stateMachineController);
        SetupMovingType();

        // Debug.Log($"{currentAIPlayer.name} - Patrolling state.....");
    }

    public override void OnCheckingState(StateMachineController stateMachineController)
    {
    }

    private void SetupMovingType()
    {
        _iMovingSystemAI = new SeekerFindingSystem();

        _iMovingSystemAI.CurrentAIPlayer = currentAIPlayer;
        _iMovingSystemAI.AIController = _aiController;

        // Debug.Log($"{currentAIPlayer.name} - Seeker Moving System");
    }
}