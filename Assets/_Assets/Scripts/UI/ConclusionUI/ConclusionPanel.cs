using TMPro;
using UnityEngine;

public class ConclusionPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _rescuedCoin;
    [SerializeField] private TMP_Text _collectedCoin;

    private GameplayManager GameplayManager => GameplayManager.Instance;

    private void OnEnable()
    {
        SetupUI();
    }

    private void SetupUI()
    {
        long rescuedCoin = GameplayManager.GetCoin(eAddingCoinType.RESCUE_BONUS_COIN);
        long collectedCoin = GameplayManager.GetCoin(eAddingCoinType.PICK_UP_COIN);

        _rescuedCoin.text = rescuedCoin.ToString();
        _collectedCoin.text = collectedCoin.ToString();
    }
}