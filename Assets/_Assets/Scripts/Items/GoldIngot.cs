using UnityEngine;

public class GoldIngot : MonoBehaviour, IPickupable
{
    public void DoPickedUpFuction(GameObject gameObject)
    {
        Debug.Log("You have just earned Gold Ingot");
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
    }

    public void SetPickupVFX()
    {
        
    }
}