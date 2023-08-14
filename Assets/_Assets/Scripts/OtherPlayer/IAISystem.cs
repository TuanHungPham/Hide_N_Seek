using UnityEngine;

public interface IAISystem
{
    Vector3 Destination { get; set; }
    Transform CurrentAIPlayer { get; set; }
    AIController AIController { get; set; }
    void HandleGettingDestination();
    void SetDestination();
    void SetInitialDestination();
}