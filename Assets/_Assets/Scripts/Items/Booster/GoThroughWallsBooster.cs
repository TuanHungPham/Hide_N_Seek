using UnityEngine;

public class GoThroughWallsBooster : MonoBehaviour, IPickupable
{
    [SerializeField] private BoosterID _boosterID;

    public void DoPickedUpFuction(GameObject gameObject)
    {
        Debug.Log("You can Going Through Wall");
        PlayerController playerController = gameObject.GetComponent<PlayerController>();

        playerController.SetCurrentUsingBooster(_boosterID);
    }

    public void DestroyItem()
    {
    }

    public void SetPickupVFX()
    {
        
    }
}