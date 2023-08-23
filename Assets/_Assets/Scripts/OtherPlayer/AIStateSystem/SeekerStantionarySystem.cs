using UnityEngine;

public class SeekerStantionarySystem : IStantionarySystemAI
{
    public AIController AIController { get; set; }
    public Transform CurrentAIPlayer { get; set; }

    private bool AnyUncaughtHiderLeft()
    {
        foreach (var player in GameplaySystem.Instance.GetHiderList())
        {
            Controller controller = player.GetComponent<Controller>();
            if (controller.GetInGameState().IsCaught()) continue;

            return true;
        }

        return false;
    }

    public bool CanChangeState()
    {
        if (!GameplaySystem.Instance.IsGameStarting()) return false;
        else if (AnyUncaughtHiderLeft()) return true;

        return false;
    }
}