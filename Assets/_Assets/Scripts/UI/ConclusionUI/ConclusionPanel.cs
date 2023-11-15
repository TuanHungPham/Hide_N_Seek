using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class ConclusionPanel : MonoBehaviour
{
    [SerializeField] private string _rescuedBonusText;
    [SerializeField] private string _caughtBonusText;
    [SerializeField] private TMP_Text _bonusCoin;
    [SerializeField] private TMP_Text _collectedCoin;
    [SerializeField] private TMP_Text _bonusText;

    private GameplayManager GameplayManager => GameplayManager.Instance;
    private GameplaySystem GameplaySystem => GameplaySystem.Instance;

    private void OnEnable()
    {
        SetupUI();
    }

    private void SetupUI()
    {
        if (GameplaySystem.IsSeekerGameplay())
        {
            _bonusText.text = _caughtBonusText;
        }
        else
        {
            _bonusText.text = _rescuedBonusText;
        }

        long bonusCoin = GameplayManager.GetCoin(eAddingCoinType.INTERACTING_BONUS_COIN);
        long collectedCoin = GameplayManager.GetCoin(eAddingCoinType.PICK_UP_COIN);

        _bonusCoin.text = bonusCoin.ToString();
        _collectedCoin.text = collectedCoin.ToString();
    }
}