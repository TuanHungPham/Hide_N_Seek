using UnityEngine;

public class BuyMoneyButton : MonoBehaviour
{
    public void BuyCoin()
    {
        GameplayManager.Instance.AddCoin(100);
    }
}