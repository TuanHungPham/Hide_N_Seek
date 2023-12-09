using UnityEngine;
using TigerForge;
using UnityEngine.AI;

public class InGameState : MonoBehaviour
{
    #region private

    [Header("For Hider")] [SerializeField] private bool _isCaught;
    [SerializeField] private bool _isDetected;
    [SerializeField] private bool _isMakingFootstep;
    [SerializeField] private BeingFoundHandler _beingFoundHandler;

    [Space(20)] [Header("For Seeker")] [SerializeField]
    private bool _isSeeker;

    [SerializeField] private bool _isTriggered;
    [SerializeField] private bool _isHearingSomething;
    [SerializeField] private GameObject _seekerVision;
    [SerializeField] private GameObject _triggerSystem;

    [Space(20)] [Header("For Both")] [Space(20)] [SerializeField]
    private FootPrintSystem _footPrintSystem;

    [SerializeField] private AudioSource _footstepSound;

    [SerializeField] private Controller _controller;
    private SoundManager SoundManager => SoundManager.Instance;

    #endregion

    private void Awake()
    {
        ListenEvent();
        LoadComponents();
    }

    private void LoadComponents()
    {
        _footPrintSystem = GetComponentInChildren<FootPrintSystem>();
        _footstepSound = GetComponentInChildren<AudioSource>();

        _footstepSound.enabled = false;
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
        CheckDetectedState();
    }

    private void CheckDetectedState()
    {
        if (!_isDetected) return;

        if (!_footPrintSystem.FeetIsPainted())
        {
            _isDetected = false;
        }
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
        return _footPrintSystem.FeetIsPainted();
    }

    public bool IsMakingFootstep()
    {
        return _isMakingFootstep;
    }

    public bool IsHearingSomething()
    {
        return _isHearingSomething;
    }

    public void SetSeekerState(bool set)
    {
        _isSeeker = set;
    }

    public void SetCaughtState(bool set)
    {
        // _controller.SetIdleAnimationState();
        _isCaught = set;
        _beingFoundHandler.SetupBeingFoundState(set);
    }

    public void SetTriggerState(bool set)
    {
        _isTriggered = set;
        // _exclamationMark.gameObject.SetActive(set);
    }

    public void SetDetectedState(bool set)
    {
        _isDetected = set;
    }

    public void SetFeetIsPainted(bool set, Material material)
    {
        if (_isCaught) return;

        _footPrintSystem.SetFootPrint(set, material);
    }

    public void SetIsMakingFootstep(bool set)
    {
        float random = Random.Range(0f, 1f);
        if (set && random <= 0.3f)
        {
            _footstepSound.enabled = true;
        }
        else
        {
            _footstepSound.enabled = false;
        }

        _isMakingFootstep = set;
    }

    public void SetIsHearingSomething(bool set)
    {
        _isHearingSomething = set;
        // _exclamationMark.gameObject.SetActive(set);
    }
}