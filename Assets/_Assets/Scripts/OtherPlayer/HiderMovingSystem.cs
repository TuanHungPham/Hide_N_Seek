using System.Collections.Generic;
using UnityEngine;

public class HiderMovingSystem : IAISystem
{
    public Vector3 Destination { get; private set; }
    public Transform CurrentAIPlayer { get; set; }
    public AIController _aiController { get; set; }

    public void HandleGettingDestination()
    {
        HideFromSeeker();
    }

    private void HideFromSeeker()
    {
        if (!CanChangePointToHide()) return;

        SetDestination();
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
        Vector3 seekerCurrentPos = GameplaySystem.Instance.GetCurrentSeekerPosition();

        float distance = Vector3.Distance(seekerCurrentPos, newDestination);

        if (distance < 20) return;

        Destination = newDestination;
    }

    private bool CanChangePointToHide()
    {
        Vector3 seekerCurrentPos = GameplaySystem.Instance.GetCurrentSeekerPosition();
        float distanceToSeeker = Vector3.Distance(CurrentAIPlayer.position, seekerCurrentPos);
        float distanceToPoint = Vector3.Distance(CurrentAIPlayer.position, Destination);

        if (distanceToSeeker <= 15 && distanceToPoint <= 2) return true;

        return false;
    }
}