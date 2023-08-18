using UnityEngine;

public class Key : MonoBehaviour, IPickupable
{
    public void DoPickedUpFuction()
    {
        Debug.Log("You have just picked up a Key");
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
    }
}