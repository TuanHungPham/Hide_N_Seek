using UnityEngine;

public class GameplayTimeSystem : MonoBehaviour
{
    [SerializeField] private float _hidingTime;
    [SerializeField] private float _timer;
    [SerializeField] private bool _isInHidingTimer;

    private void Awake()
    {
        LoadComponents();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        _isInHidingTimer = true;
        _timer = _hidingTime;
    }

    private void Update()
    {
        RunHidingTimer();
    }

    private void RunHidingTimer()
    {
        if (!GameplaySystem.Instance.IsGameStarting()) return;

        if (_timer > 0)
        {
            _isInHidingTimer = true;
            _timer -= Time.deltaTime;
            return;
        }

        _isInHidingTimer = false;
    }

    public bool IsInHidingTimer()
    {
        return _isInHidingTimer;
    }

    public float GetHidingTime()
    {
        return _hidingTime;
    }

    public float GetHidingTimer()
    {
        return _timer;
    }
}