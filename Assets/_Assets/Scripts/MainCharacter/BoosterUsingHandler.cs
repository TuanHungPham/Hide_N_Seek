using UnityEngine;

public class BoosterUsingHandler : MonoBehaviour
{
    [SerializeField] private float _boosterTimer;
    [SerializeField] private float _boosterTime;

    [Space(20)] [Header("BoosterAbility")] [SerializeField]
    private BoosterID _currentUsingBooster;

    [SerializeField] private bool _canGoThroughWall;
    [SerializeField] private bool _beInvisible;

    private void Update()
    {
        RunBoosterTimer();
    }

    private void RunBoosterTimer()
    {
        if (_currentUsingBooster == BoosterID.NONE) return;

        if (_boosterTimer > 0)
        {
            _boosterTimer -= Time.deltaTime;
            return;
        }

        _currentUsingBooster = BoosterID.NONE;
    }

    public void SetCurrentUsingBooster(BoosterID boosterID)
    {
        _currentUsingBooster = boosterID;
        SetupAbility();
    }

    private void SetupAbility()
    {
        _boosterTimer = _boosterTime;
        switch (_currentUsingBooster)
        {
            case BoosterID.CAN_GO_THROUGH_WALLS:
                _canGoThroughWall = true;
                break;
            case BoosterID.INVISIBLE:
                _beInvisible = true;
                break;
        }
    }
}