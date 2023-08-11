using System;
using UnityEngine;

public class InGameState : MonoBehaviour
{
    #region private

    [SerializeField] private bool _isSeeker;
    [SerializeField] private bool _isCaught;
    [SerializeField] private bool _isTriggered;
    [SerializeField] private bool _isDetected;

    [Space(20)] [SerializeField] private GameObject cage;

    #endregion

    private void Awake()
    {
    }

    private void Start()
    {
        SetCaughtState(false);
    }

    public bool IsSeeker()
    {
        return _isSeeker;
    }

    public bool IsCaught()
    {
        return _isCaught;
    }

    public bool IsTrigger()
    {
        return _isTriggered;
    }

    public bool IsDetected()
    {
        return _isDetected;
    }

    public void SetCaughtState(bool set)
    {
        _isCaught = set;
        cage.gameObject.SetActive(set);
    }
}