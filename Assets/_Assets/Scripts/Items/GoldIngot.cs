using UnityEngine;

public class GoldIngot : MonoBehaviour, IPickupable
{
    [SerializeField] private long _addingCoinQuantity;

    public void DoPickedUpFuction(GameObject obj)
    {
        Debug.Log("You have just earned Gold Ingot");
        GameplayManager.Instance.AddCoin(eAddingCoinType.PICK_UP_COIN, _addingCoinQuantity);
    }

    public void DestroyItem()
    {
        gameObject.SetActive(false);
    }

    public void SetPickupVFX()
    {
    }
}