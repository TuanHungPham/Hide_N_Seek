using TigerForge;
using UnityEngine;
using UnityEngine.Serialization;

public class GameplayTimeSystem : MonoBehaviour
{
    [Header("Hiding Time")] [SerializeField]
    private float _hidingTime;

    [SerializeField] private float _timer;
    [SerializeField] private bool _isInHidingTimer;

    [Space(20)] [Header("Gameplay Time")] [SerializeField]
    private float _gamePlayTime;

    [SerializeField] private float _gamePlayTimer;
    [SerializeField] private bool _isTimeUp;
    [SerializeField] private Timer timer;

    private Timer Timer
    {
        get
        {
            if (timer == null)
                timer = new Timer();
            return timer;
        }
    }

    private bool HasPastInterval => Timer.HasPastInterval();
    private SoundManager SoundManager => SoundManager.Instance;

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

        _gamePlayTimer = _gamePlayTime;
        _timer = _hidingTime;
    }

    private void Update()
    {
        RunHidingTimer();
        RunGameplayTimer();
    }

    private void RunHidingTimer()
    {
        if (!GameplaySystem.Instance.IsGameStarting() || !_isInHidingTimer) return;

        if (_timer > 0)
        {
            _isInHidingTimer = true;
            _timer -= Time.deltaTime;
            return;
        }

        _isInHidingTimer = false;
        EmitEndHidingTimeEvent();
    }

    private void RunGameplayTimer()
    {
        if (GameplaySystem.Instance.IsInHidingTimer() || _isTimeUp) return;

        if (_gamePlayTimer <= 0)
        {
            _gamePlayTimer = 0;
            _isTimeUp = true;
            EmitEndGameTimeEvent();
            return;
        }

        _isTimeUp = false;
        _gamePlayTimer -= Time.deltaTime;

        if (_gamePlayTimer > 10) return;
        if (HasPastInterval)
        {
            SoundManager.PlaySFX(eSoundType.TIME_TICK, Vector3.zero);
        }
    }

    private void EmitEndHidingTimeEvent()
    {
        EventManager.EmitEvent(EventID.END_HIDING_TIME);
    }

    private void EmitEndGameTimeEvent()
    {
        EventManager.EmitEvent(EventID.END_GAME_TIME);
    }

    public bool IsInHidingTimer()
    {
        return _isInHidingTimer;
    }

    public bool IsTimeUp()
    {
        return _isTimeUp;
    }

    public float GetGameplayTimer()
    {
        return _gamePlayTimer;
    }

    public float GetGameplayTime()
    {
        return _gamePlayTime;
    }

    public float GetHidingTime()
    {
        return _hidingTime;
    }

    public float GetHidingTimer()
    {
        return _timer;
    }

    public void SetGameplayTimer(float gameplayTimer)
    {
        _gamePlayTimer = gameplayTimer;
    }
}