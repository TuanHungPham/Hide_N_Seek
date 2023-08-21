using UnityEngine;

public class SeekerFindingSystem : IMovingSystemAI
{
    public Vector3 Destination { get; set; }
    public Transform CurrentAIPlayer { get; set; }
    public AIController AIController { get; set; }

    public void HandleGettingDestination()
    {
        // SetDestination();
    }

    public void SetDestination()
    {
        Transform footstepSoundMaking = AIController.GetTriggeredSystem().GetFootstepSoundMaking();
        Destination = footstepSoundMaking.position;
    }

    public void SetInitialDestination()
    {
        SetDestination();
    }

    public bool CanChangeState()
    {
        return false;
    }
}