using System;
using UnityEngine;

public class Model : MonoBehaviour
{
    #region private

    [SerializeField] private Controller _controller;

    #endregion

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
        _controller = GetComponentInParent<Controller>();
    }

    private void Start()
    {
        SetupModelSize();
    }

    private void SetupModelSize()
    {
        if (!_controller.GetInGameState().IsSeeker()) return;

        transform.localScale *= 3;
    }
}