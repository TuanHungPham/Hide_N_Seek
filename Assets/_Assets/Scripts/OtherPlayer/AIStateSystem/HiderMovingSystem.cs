using System.Collections.Generic;
using UnityEngine;

public class HiderMovingSystem : IMovingSystemAI
{
    public Vector3 Destination { get; set; }
    public Transform CurrentAIPlayer { get; set; }
    public AIController AIController { get; set; }

    public void HandleGettingDestination()
    {
        HideFromSeeker();
    }

    private void HideFromSeeker()
    {
        if (!CanChangeState()) return;

        SetDestination();
    }

    public void SetInitialDestination()
    {
        Destination = CurrentAIPlayer.position;
    }

    public bool CanChangeState()
    {
        // Vector3 seekerCurrentPos = GameplaySystem.Instance.GetNearestSeekerPosition(CurrentAIPlayer);
        var currentPosition = CurrentAIPlayer.position;

        // float distanceToSeeker = Vector3.Distance(currentPosition, seekerCurrentPos);
        float distanceToPoint = Vector3.Distance(currentPosition, Destination);

        if (distanceToPoint <= 2 || AIController.GetInGameState().IsCaught()) return true;

        return false;
    }

    public void SetDestination()
    {
        List<Vector3> limitPosList = MapLevelSystem.Instance.GetLimitPosList();

        Vector3 maxX = limitPosList.Find((x) => x.x > 0);
        Vector3 minX = limitPosList.Find((x) => x.x < 0);
        Vector3 maxZ = limitPosList.Find((x) => x.z > 0);
        Vector3 minZ = limitPosList.Find((x) => x.z < 0);

        float posX = Random.Range(minX.x, maxX.x);
        float posZ = Random.Range(minZ.z, maxZ.z);

        Vector3 newDestination = new Vector3(posX, 0, posZ);
        Vector3 seekerCurrentPos = GameplaySystem.Instance.GetNearestSeekerPosition(CurrentAIPlayer);

        float distance = Vector3.Distance(seekerCurrentPos, newDestination);

        if (distance < 20) return;

        Destination = newDestination;
    }
}