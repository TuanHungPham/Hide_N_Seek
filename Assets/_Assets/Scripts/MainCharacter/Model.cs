using System;
using TigerForge;
using UnityEngine;

public class Model : MonoBehaviour
{
    #region private

    [SerializeField] private Controller _controller;
    [SerializeField] private MeshRenderer _meshRenderer;

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
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.SETTED_UP_GAMEPLAY, SetupModelSize);
    }

    private void Update()
    {
        HandleInvisibleModelInRuntime();
    }

    private void HandleInvisibleModelInRuntime()
    {
        if (!GameplaySystem.Instance.IsSeekerGameplay() || _controller.GetInGameState().IsSeeker()) return;

        if (_controller.GetInGameState().IsCaught() || GameplaySystem.Instance.IsInHidingTimer())
        {
            _meshRenderer.enabled = true;
            return;
        }

        _meshRenderer.enabled = false;
    }

    private void SetupModelSize()
    {
        if (!_controller.GetInGameState().IsSeeker()) return;

        transform.localScale *= 1.5f;
    }
}