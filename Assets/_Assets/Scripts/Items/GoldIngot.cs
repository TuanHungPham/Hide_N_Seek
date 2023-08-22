using UnityEngine;

public class GoldIngot : MonoBehaviour, IPickupable
{
    public void DoPickedUpFuction()
    {
        Debug.Log("You have just earned Gold Ingot");
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
    }
}