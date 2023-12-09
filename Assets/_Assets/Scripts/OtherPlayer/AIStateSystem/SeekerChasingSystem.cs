using UnityEngine;

public class SeekerChasingSystem : IMovingSystemAI
{
    public Vector3 Destination { get; set; }
    public Transform CurrentAIPlayer { get; set; }
    public AIController AIController { get; set; }

    public void HandleGettingDestination()
    {
        SetDestination();
    }

    public void SetDestination()
    {
       
    }

    public void SetInitialDestination()
    {
    }

    public bool CanChangeState()
    {
        if (IsChasingAnyone()) return false;

        return true;
    }

    private bool IsChasingAnyone()
    {
        // Debug.Log("AI is chasing Hider...");
        foreach (Transform player in GameplaySystem.Instance.GetHiderList())
        {
            Controller controller = player.GetComponent<Controller>();

            if (!CanChaseHider(controller)) continue;

            Destination = player.position;
            return true;
        }

        AIController.GetInGameState().SetTriggerState(false);

        return false;
    }

    private bool CanChaseHider(Controller controller)
    {
        if (controller.GetInGameState().IsCaught()) return false;
        else if (!controller.GetInGameState().IsDetected()) return false;

        return true;
    }
}