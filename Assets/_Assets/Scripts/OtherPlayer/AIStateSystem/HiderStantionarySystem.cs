using UnityEngine;

public class HiderStantionarySystem : IStantionarySystemAI
{
    public AIController AIController { get; set; }
    public Transform CurrentAIPlayer { get; set; }

    public bool CanChangeState()
    {
        if (AIController.GetInGameState().IsCaught()) return false;

        Vector3 seekerCurrentPos = GameplaySystem.Instance.GetNearestSeekerPosition(CurrentAIPlayer);
        var currentPosition = CurrentAIPlayer.position;

        float distanceToSeeker = Vector3.Distance(currentPosition, seekerCurrentPos);

        if (distanceToSeeker <= 20) return true;

        return false;
    }
}