using UnityEngine;

public interface IPickupable
{
    void DoPickedUpFuction(GameObject obj);
    void DestroyItem();
    void SetPickupVFX();
}