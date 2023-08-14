using System.Collections.Generic;
using UnityEngine;

public class SeekerMovingSystem : IAISystem
{
    public Vector3 Destination { get; set; }
    public Transform CurrentAIPlayer { get; set; }
    public AIController AIController { get; set; }

    public void HandleGettingDestination()
    {
        // Debug.Log("AI is getting Destination...");
        if (AIController.GetInGameState().IsTrigger())
        {
            GetDestinationToChase();
        }
        else
        {
            GetDestinationToPatroll();
        }
    }

    private void GetDestinationToChase()
    {
        // Debug.Log("AI is chasing Hider...");
        foreach (Transform player in GameplaySystem.Instance.GetAllPlayerList())
        {
            Controller controller = player.GetComponent<Controller>();

            if (!CanChaseHider(controller)) continue;

            Destination = player.position;
        }
    }

    private void GetDestinationToPatroll()
    {
        if (!CanChangePointToPatrol()) return;

        Debug.Log("AI is patrolling...");
        SetDestination();
    }

    public void SetInitialDestination()
    {
        Destination = CurrentAIPlayer.position;
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

    private bool CanChaseHider(Controller controller)
    {
        if (controller.GetInGameState().IsCaught()) return false;
        else if (controller.GetInGameState().IsSeeker()) return false;
        else if (!controller.GetInGameState().IsDetected()) return false;

        return true;
    }

    private bool CanChangePointToPatrol()
    {
        float distance = Vector3.Distance(CurrentAIPlayer.position, Destination);
        if (distance < 1) return true;

        return false;
    }
}