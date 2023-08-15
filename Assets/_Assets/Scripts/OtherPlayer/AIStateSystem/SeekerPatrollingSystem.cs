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
        if (!CanChangeState()) return;

        Debug.Log("AI is patrolling...");
        SetDestination();
    }

    public void SetInitialDestination()
    {
        Destination = CurrentAIPlayer.position;
    }

    public bool CanChangeState()
    {
        float distance = Vector3.Distance(CurrentAIPlayer.position, Destination);

        if (distance < 1 || AIController.GetInGameState().IsSeekerWaitingTime()) return true;

        return false;
    }

    public void SetDestination()
    {
        List<Vector3> patrolPointList = MapLevelSystem.Instance.GetPatrolPointList();

        int index = Random.Range(0, patrolPointList.Count);
        Vector3 newDestination = patrolPointList[index];

        float distance = Vector3.Distance(CurrentAIPlayer.position, newDestination);

        if (distance < 5) return;

        Destination = newDestination;
    }
}