using UnityEngine;
using TigerForge;
using UnityEngine.Serialization;

public class InGameState : MonoBehaviour
{
    #region private

    [SerializeField] private bool _isSeeker;
    [SerializeField] private bool _isCaught;
    [SerializeField] private bool _isTriggered;
    [SerializeField] private bool _isDetected;
    [SerializeField] private bool _feetIsPainted;
    [SerializeField] private bool _isMakingFootstep;

    [Space(20)] [SerializeField] private float _detectedTimer;
    [SerializeField] private float _detectedTime;

    [Space(20)] [SerializeField] private GameObject cage;
    [SerializeField] private GameObject _seekerVision;
    [SerializeField] private GameObject _triggerSystem;

    #endregion

    private void Awake()
    {
        ListenEvent();
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.SETTED_UP_GAMEPLAY, CheckSeekerVision);
    }

    private void Start()
    {
        SetCaughtState(false);
    }

    private void Update()
    {
        CheckDetectedTime();
    }

    private void CheckDetectedTime()
    {
        if (!_isDetected) return;

        if (_detectedTimer <= 0)
        {
            _isDetected = false;
            _feetIsPainted = false;
            return;
        }

        _detectedTimer -= Time.deltaTime;
    }

    private void CheckSeekerVision()
    {
        if (_isSeeker)
        {
            _seekerVision.gameObject.SetActive(true);
            if (_triggerSystem != null)
            {
                _triggerSystem.gameObject.SetActive(true);
            }

            return;
        }

        _seekerVision.gameObject.SetActive(false);
        if (_triggerSystem != null)
        {
            _triggerSystem.gameObject.SetActive(false);
        }
    }

    public bool IsSeeker()
    {
        return _isSeeker;
    }

    public bool IsCaught()
    {
        return _isCaught;
    }

    public bool IsTriggered()
    {
        return _isTriggered;
    }

    public bool IsDetected()
    {
        return _isDetected;
    }

    public bool FeetIsPainted()
    {
        return _feetIsPainted;
    }

    public bool IsMakingFootstep()
    {
        return _isMakingFootstep;
    }

    public void SetSeekerState(bool set)
    {
        _isSeeker = set;
    }

    public void SetCaughtState(bool set)
    {
        _isCaught = set;
        cage.gameObject.SetActive(set);
    }

    public void SetTriggerState(bool set)
    {
        _isTriggered = set;
    }

    public void SetDetectedState(bool set)
    {
        _detectedTimer = _detectedTime;
        _isDetected = set;
    }

    public void SetFeetIsPainted(bool set)
    {
        _feetIsPainted = set;
    }

    public void SetIsMakingFootstep(bool set)
    {
        _isMakingFootstep = set;
    }
}