using UnityEngine;

public class StartingGameSystem : MonoBehaviour
{
    #region private

    [SerializeField] private float _hidingTime;
    [SerializeField] private float _timer;
    [SerializeField] private bool _isInHidingTimer;

    [Space(20)] [SerializeField] private SetupGameplayType _setupGameplayType;
    [SerializeField] private SetupStartingSpawn _setupStartingSpawn;

    #endregion

    private void Awake()
    {
        LoadComponents();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void Update()
    {
        RunHidingTimer();
    }

    private void RunHidingTimer()
    {
        if (_timer > 0)
        {
            _isInHidingTimer = true;
            _timer -= Time.deltaTime;
            return;
        }

        _isInHidingTimer = false;
    }

    private void LoadComponents()
    {
        _setupGameplayType = GetComponentInChildren<SetupGameplayType>();
        _setupStartingSpawn = GetComponentInChildren<SetupStartingSpawn>();

        _timer = _hidingTime;
    }

    public bool IsInHidingTimer()
    {
        return _isInHidingTimer;
    }

    public Transform GetMainCharacterReference()
    {
        return _setupStartingSpawn.GetMainCharacterReference();
    }

    public float GetHidingTime()
    {
        return _hidingTime;
    }

    public bool IsSeekerGameplay()
    {
        return _setupGameplayType.IsSeekerGameplay();
    }
}