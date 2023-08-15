using UnityEngine;
using TigerForge;

public class InGameState : MonoBehaviour
{
    #region private

    [SerializeField] private bool _isSeeker;
    [SerializeField] private bool _isCaught;
    [SerializeField] private bool _isTriggered;
    [SerializeField] private bool _isDetected;
    [SerializeField] private bool _isSeekerWaitingTime;

    [Space(20)] [SerializeField] private GameObject cage;
    [SerializeField] private GameObject _seekerVision;

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
        CheckHidingTime();
    }

    private void CheckHidingTime()
    {
        if (!_isSeeker) return;

        if (GameplaySystem.Instance.IsInHidingTimer())
        {
            // Debug.Log("Checking hiding time 1...");
            _isSeekerWaitingTime = true;
        }
        else
        {
            // Debug.Log("Checking hiding time 2...");
            _isSeekerWaitingTime = false;
        }
    }

    private void CheckSeekerVision()
    {
        if (_isSeeker)
        {
            _seekerVision.gameObject.SetActive(true);
            return;
        }

        _seekerVision.gameObject.SetActive(false);
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

    public bool IsSeekerWaitingTime()
    {
        return _isSeekerWaitingTime;
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
}