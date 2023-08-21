using System.Collections.Generic;
using UnityEngine;

public class SeekerPatrollingSystem : IMovingSystemAI
{
    public Vector3 Destination { get; set; }
    public Transform CurrentAIPlayer { get; set; }
    public AIController AIController { get; set; }

    public void HandleGettingDestination()
    {
        // Debug.Log("AI is getting Destination...");
        GetDestinationToPatroll();
    }

    private void GetDestinationToPatroll()
    {
        if (!IsNearToPointDestination()) return;

        // Debug.Log("AI is patrolling...");
        SetDestination();
    }

    public void SetInitialDestination()
    {
        Destination = CurrentAIPlayer.position;
    }

    private bool IsNearToPointDestination()
    {
        var currentPosition = CurrentAIPlayer.position;
        float distanceToPoint = Vector3.Distance(currentPosition, Destination);

        if (distanceToPoint <= Distance.DISTANCE_TO_POINT_DESTINATION) return true;
        return false;
    }

    public bool CanChangeState()
    {
        return false;
    }

    public void SetDestination()
    {
        List<Vector3> patrolPointList = MapLevelSystem.Instance.GetPatrolPointList();

        int index = Random.Range(0, patrolPointList.Count);
        Vector3 newDestination = patrolPointList[index];

        float distance = Vector3.Distance(CurrentAIPlayer.position, newDestination);

        if (distance < Distance.DISTANCE_TO_CURRENT_POINT) return;

        Destination = newDestination;
    }
}