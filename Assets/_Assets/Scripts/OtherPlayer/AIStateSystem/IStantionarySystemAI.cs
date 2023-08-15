using UnityEngine;

public interface IStantionarySystemAI
{
    AIController AIController { get; set; }
    Transform CurrentAIPlayer { get; set; }
    bool CanChangeState();
}