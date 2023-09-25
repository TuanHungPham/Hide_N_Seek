using UnityEngine;

public class Key : MonoBehaviour, IPickupable
{
    public void DoPickedUpFuction(GameObject gameObject)
    {
        Debug.Log("You have just picked up a Key");
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
    }

    public void SetPickupVFX()
    {
    }
}