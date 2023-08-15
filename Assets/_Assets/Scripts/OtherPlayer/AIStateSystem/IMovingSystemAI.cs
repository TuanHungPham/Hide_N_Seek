using UnityEngine;

public interface IMovingSystemAI
{
    Vector3 Destination { get; set; }
    Transform CurrentAIPlayer { get; set; }
    AIController AIController { get; set; }
    void HandleGettingDestination();
    void SetDestination();
    void SetInitialDestination();
    bool CanChangeState();
}