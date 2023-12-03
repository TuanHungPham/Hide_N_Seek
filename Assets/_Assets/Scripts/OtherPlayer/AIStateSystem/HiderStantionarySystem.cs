using UnityEngine;

public class HiderStantionarySystem : IStantionarySystemAI
{
    public AIController AIController { get; set; }
    public Transform CurrentAIPlayer { get; set; }

    private GameplaySystem GameplaySystem => GameplaySystem.Instance;

    public bool CanChangeState()
    {
        if (!GameplaySystem.IsGameStarting()) return false;
        if (AIController.GetInGameState().IsCaught()) return false;

        return true;
    }
}