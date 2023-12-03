using UnityEngine;

public class HiderRescuingSystem : IMovingSystemAI
{
    public Vector3 Destination { get; set; }
    public Transform CurrentAIPlayer { get; set; }
    public AIController AIController { get; set; }

    private GameplaySystem GameplaySystem => GameplaySystem.Instance;

    public void HandleGettingDestination()
    {
        SetDestination();
    }

    public void SetDestination()
    {
        float minDistance = 0;
        bool isInitializedMinDistance = false;

        foreach (var hider in GameplaySystem.GetHiderCaughtList())
        {
            Controller hiderController = hider.GetComponent<Controller>();
            if (!hiderController.GetInGameState().IsCaught()) continue;

            if (!CanComeToRescue(hider))
            {
                Destination = CurrentAIPlayer.position;
                continue;
            }

            SetDestinationToNearest(hider, ref minDistance, ref isInitializedMinDistance);
        }
    }

    private void SetDestinationToNearest(Transform hider, ref float minDistance, ref bool isInitializedMinDistance)
    {
        float distanceToCaughtHider = Vector3.Distance(CurrentAIPlayer.position, hider.position);
        if (distanceToCaughtHider >= minDistance)
        {
            if (isInitializedMinDistance) return;

            Debug.Log("Initial Recuing Set...");
            isInitializedMinDistance = true;
            minDistance = distanceToCaughtHider;
            Destination = hider.position;
        }
        else
        {
            minDistance = distanceToCaughtHider;
            Destination = hider.position;
        }
    }

    private bool CanComeToRescue(Transform caughtHider)
    {
        var nearestSeeker = GameplaySystem.GetNearestSeekerPosition(caughtHider);
        var hiderPos = caughtHider.position;

        float distance = Vector3.Distance(hiderPos, nearestSeeker);

        if (distance <= Distance.DISTANCE_FROM_POINT_TO_SEEKER) return false;

        return true;
    }

    private bool IsAnyHiderCaught()
    {
        int numberOfCaughtHider = GameplaySystem.GetNumberOfCaughtHider();
        if (numberOfCaughtHider > 0) return true;

        return false;
    }

    private bool IsNearAnySeeker()
    {
        Vector3 seekerCurrentPos = GameplaySystem.GetNearestSeekerPosition(CurrentAIPlayer);
        var currentPosition = CurrentAIPlayer.position;

        float distanceToSeeker = Vector3.Distance(currentPosition, seekerCurrentPos);

        if (distanceToSeeker <= Distance.DISTANCE_TO_SEEKER) return true;

        return false;
    }

    public void SetInitialDestination()
    {
    }

    public bool CanChangeState()
    {
        if (AIController.GetInGameState().IsCaught()) return true;
        else if (IsNearAnySeeker()) return true;
        else if (IsAnyHiderCaught()) return false;

        return true;
    }
}