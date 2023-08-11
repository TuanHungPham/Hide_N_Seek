using UnityEngine;

public interface IAIMovingSystem
{
    Transform Destination { get; }
    void Move();
    void HandleGettingDestination();
    Transform GetDestination();
}