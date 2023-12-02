using UnityEngine;

public class Key : MonoBehaviour, IPickupable
{
    public void DoPickedUpFuction(GameObject gameObject)
    {
        Debug.Log("You have just picked up a Key");
    }

    public void DestroyItem()
    {
        gameObject.SetActive(false);
    }

    public void SetPickupVFX()
    {
    }
}