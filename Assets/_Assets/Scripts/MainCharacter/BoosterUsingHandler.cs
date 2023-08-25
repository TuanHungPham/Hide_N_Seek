using UnityEngine;

public class BoosterUsingHandler : MonoBehaviour
{
    [SerializeField] private float _boosterTimer;
    [SerializeField] private float _boosterTime;

    [Space(20)] [SerializeField] private GameObject _mainCharacter;

    [Space(20)] [Header("BoosterAbility")] [SerializeField]
    private BoosterID _currentUsingBooster;

    [SerializeField] private bool _canGoThroughWall;
    [SerializeField] private bool _isInvisible;
    private IBoosterAbility _boosterAbility;

    private void Update()
    {
        HandleUsingBooster();
    }

    private void HandleUsingBooster()
    {
        if (_currentUsingBooster == BoosterID.NONE) return;
        RunBoosterTimer();
        ReceiveBoosterAbility();
    }

    private void ReceiveBoosterAbility()
    {
        _boosterAbility.DoAbility(_mainCharacter);
    }

    private void RunBoosterTimer()
    {
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
                _boosterAbility = new GoingThroughWalls();
                break;
            case BoosterID.INVISIBLE:
                _isInvisible = true;
                break;
        }
    }

    public bool IsInvisible()
    {
        return _isInvisible;
    }
}