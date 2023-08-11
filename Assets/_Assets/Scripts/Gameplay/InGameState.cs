using System;
using UnityEngine;

public class InGameState : MonoBehaviour
{
    #region private

    [SerializeField] private bool isSeeker;
    [SerializeField] private bool isCaught;

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
        return isSeeker;
    }

    public bool IsCaught()
    {
        return isCaught;
    }

    public void SetCaughtState(bool set)
    {
        isCaught = set;
        cage.gameObject.SetActive(set);
    }
}