using UnityEngine;

public class BuyMoneyButton : MonoBehaviour
{
    public void BuyCoin()
    {
        GameplayManager.Instance.AddCoin(eAddingCoinType.PICK_UP_COIN, 100);
    }
}