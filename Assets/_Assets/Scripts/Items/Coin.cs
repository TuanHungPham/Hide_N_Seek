using UnityEngine;

public class Coin : MonoBehaviour, IPickupable
{
    public void DoPickedUpFuction()
    {
        Debug.Log("You have just earned some Coins");
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
    }
}