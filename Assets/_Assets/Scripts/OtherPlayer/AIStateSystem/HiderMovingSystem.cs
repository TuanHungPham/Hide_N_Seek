using System.Collections.Generic;
using UnityEngine;

public class HiderMovingSystem : IMovingSystemAI
{
    public Vector3 Destination { get; set; }
    public Transform CurrentAIPlayer { get; set; }
    public AIController AIController { get; set; }

    private MapLevelSystem MapLevelSystem => MapLevelSystem.Instance;
    private GameplaySystem GameplaySystem => GameplaySystem.Instance;

    public void HandleGettingDestination()
    {
        HideFromSeeker();
    }

    private void HideFromSeeker()
    {
        if (!IsNearToPointDestination()) return;

        SetDestination();
    }

    public void SetInitialDestination()
    {
        Destination = CurrentAIPlayer.position;
    }

    public bool CanChangeState()
    {
        if (!IsNearToPointDestination()) return false;

        return true;
    }

    public void SetDestination()
    {
        List<Vector3> hidePosList = MapLevelSystem.GetPatrolPointList();

        int randomIndex = Random.Range(0, hidePosList.Count);

        Vector3 newDestination = hidePosList[randomIndex];
        Vector3 seekerCurrentPos = GameplaySystem.GetNearestSeekerPosition(CurrentAIPlayer);

        if (PointNearSeeker(seekerCurrentPos, newDestination) || PointToSeeker(seekerCurrentPos, newDestination)) return;

        Destination = newDestination;
    }

    private bool PointNearSeeker(Vector3 seekerCurrentPos, Vector3 newDestination)
    {
        float distance = Vector3.Distance(seekerCurrentPos, newDestination);

        if (distance < Distance.DISTANCE_FROM_POINT_TO_SEEKER) return true;
        return false;
    }

    private bool PointToSeeker(Vector3 seekerCurrentPos, Vector3 newDestination)
    {
        var thisAIPos = CurrentAIPlayer.position;
        Vector3 directionToPoint = (newDestination - thisAIPos).normalized;
        Vector3 directionToSeeker = (seekerCurrentPos - thisAIPos).normalized;
        float angle = Vector3.Angle(directionToPoint, directionToSeeker);

        // Debug.Log($"(HIDING) Angle to Seeker: {angle}");

        if (angle <= Distance.ANGEL_TO_SEEKER) return true;

        return false;
    }

    private bool IsNearToPointDestination()
    {
        var currentPosition = CurrentAIPlayer.position;
        float distanceToPoint = Vector3.Distance(currentPosition, Destination);

        if (distanceToPoint <= Distance.DISTANCE_TO_POINT_DESTINATION) return true;
        return false;
    }
}