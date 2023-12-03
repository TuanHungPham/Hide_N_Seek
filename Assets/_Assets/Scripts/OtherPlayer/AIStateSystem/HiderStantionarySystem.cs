using UnityEngine;

public class HiderStantionarySystem : IStantionarySystemAI
{
    public AIController AIController { get; set; }
    public Transform CurrentAIPlayer { get; set; }

    private GameplaySystem GameplaySystem => GameplaySystem.Instance;

    public bool CanChangeState()
    {
        if (!GameplaySystem.IsGameStarting()) return false;
        if (AIController.GetInGameState().IsCaught()) return false;
        if (IsAnyHiderCaught()) return true;

        Vector3 seekerCurrentPos = GameplaySystem.GetNearestSeekerPosition(CurrentAIPlayer);
        var currentPosition = CurrentAIPlayer.position;

        float distanceToSeeker = Vector3.Distance(currentPosition, seekerCurrentPos);

        if (distanceToSeeker <= Distance.DISTANCE_TO_SEEKER) return true;

        return false;
    }

    private bool IsAnyHiderCaught()
    {
        if (GameplaySystem.GetNumberOfCaughtHider() <= 0) return false;

        return true;
    }
}