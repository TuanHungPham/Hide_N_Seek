using UnityEngine;

public class SeekerFindingSystem : IMovingSystemAI
{
    public Vector3 Destination { get; set; }
    public Transform CurrentAIPlayer { get; set; }
    public AIController AIController { get; set; }

    public void HandleGettingDestination()
    {
    }

    public void SetDestination()
    {
    }

    public void SetInitialDestination()
    {
    }

    public bool CanChangeState()
    {
        return false;
    }
}