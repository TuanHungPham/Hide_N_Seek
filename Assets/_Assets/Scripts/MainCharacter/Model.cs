using System;
using TigerForge;
using UnityEngine;

public class Model : MonoBehaviour
{
    #region private

    [SerializeField] private Controller _controller;

    #endregion

    private void Awake()
    {
        LoadComponents();
        ListenEvent();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        _controller = GetComponentInParent<Controller>();
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.SETTED_UP_GAMEPLAY, SetupModelSize);
    }

    private void SetupModelSize()
    {
        if (!_controller.GetInGameState().IsSeeker()) return;

        transform.localScale *= 3;
    }
}