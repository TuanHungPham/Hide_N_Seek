using UnityEngine;

public class Coin : MonoBehaviour, IPickupable
{
    [SerializeField] private long _addingCoinQuantity;

    public void DoPickedUpFuction(GameObject gameObject)
    {
        Debug.Log("You have just earned some Coins");
        GameplayManager.Instance.AddCoin(eAddingCoinType.PICK_UP_COIN, _addingCoinQuantity);
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
    }

    public void SetPickupVFX()
    {
    }
}