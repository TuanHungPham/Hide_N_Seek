using UnityEngine;

public class SeekerStantionarySystem : IStantionarySystemAI
{
    public AIController AIController { get; set; }
    public Transform CurrentAIPlayer { get; set; }
    private GameplaySystem GameplaySystem => GameplaySystem.Instance;

    private bool AnyUncaughtHiderLeft()
    {
        int caughtNumber = GameplaySystem.GetNumberOfCaughtHider();
        int hiderNumber = GameplaySystem.GetNumberOfHider();
        if (caughtNumber < hiderNumber) return true;

        return false;
    }

    public bool CanChangeState()
    {
        if (!GameplaySystem.IsGameStarting()) return false;
        if (AnyUncaughtHiderLeft()) return true;

        return false;
    }
}