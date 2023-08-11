using UnityEngine;

public interface IAISystem
{
    Vector3 Destination { get; }
    Transform CurrentAIPlayer { get; set; }
    AIController _aiController { get; set; }
    void HandleGettingDestination();
}