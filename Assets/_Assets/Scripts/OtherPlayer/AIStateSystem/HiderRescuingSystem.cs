using UnityEngine;

public class HiderRescuingSystem : IMovingSystemAI
{
    public Vector3 Destination { get; set; }
    public Transform CurrentAIPlayer { get; set; }
    public AIController AIController { get; set; }

    public void HandleGettingDestination()
    {
        SetDestination();
    }

    public void SetDestination()
    {
        float minDistance = 0;
        bool isInitializedMinDistance = false;

        foreach (var hider in GameplaySystem.Instance.GetHiderList())
        {
            Controller hiderController = hider.GetComponent<Controller>();
            if (!hiderController.GetInGameState().IsCaught()) continue;

            if (!CanComeToRescue(hider))
            {
                Destination = CurrentAIPlayer.position;
                continue;
            }

            float distanceToCaughtHider = Vector3.Distance(CurrentAIPlayer.position, hider.position);
            if (distanceToCaughtHider >= minDistance)
            {
                if (isInitializedMinDistance) continue;

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
    }

    private bool CanComeToRescue(Transform caughtHider)
    {
        float distance = Vector3.Distance(caughtHider.position,
            GameplaySystem.Instance.GetNearestSeekerPosition(caughtHider));

        if (distance <= Distance.DISTANCE_FROM_POINT_TO_SEEKER) return false;

        return true;
    }

    private bool IsAnyHiderCaught()
    {
        foreach (var hider in GameplaySystem.Instance.GetHiderList())
        {
            if (hider == CurrentAIPlayer) continue;

            Controller hiderController = hider.GetComponent<Controller>();
            if (!hiderController.GetInGameState().IsCaught()) continue;

            return true;
        }

        return false;
    }

    private bool IsNearAnySeeker()
    {
        Vector3 seekerCurrentPos = GameplaySystem.Instance.GetNearestSeekerPosition(CurrentAIPlayer);
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