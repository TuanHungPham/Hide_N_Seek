using System;
using TigerForge;
using UnityEngine;

public class InteractingSystem : MonoBehaviour
{
    [SerializeField] private Controller _controller;
    [SerializeField] private LayerMask _layerMask;

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

    private void OnTriggerStay(Collider target)
    {
        if (GameplaySystem.Instance.IsInHidingTimer()) return;

        if ((_layerMask.value & (1 << target.gameObject.layer)) == 0) return;

        CheckObjColliderState(target.gameObject);
        // Debug.Log("Colliding...........");
    }

    private void CheckObjColliderState(GameObject target)
    {
        Controller targetController = target.GetComponent<Controller>();
        if (targetController == null) return;

        bool targetIsSeeker = targetController.GetInGameState().IsSeeker();
        bool thisObjIsSeeker = _controller.GetInGameState().IsSeeker();

        if (thisObjIsSeeker) return;

        CollideAsHider(targetIsSeeker, targetController);
    }

    private void CollideAsHider(bool targetIsSeeker, Controller targetController)
    {
        if (targetIsSeeker) return;

        if (!targetController.GetInGameState().IsCaught() || _controller.GetInGameState().IsCaught()) return;

        RescueTarget(targetController);
    }

    private void RescueTarget(Controller targetController)
    {
        targetController.SetCaughtState(false);
        EmitRescuingEvent();
    }

    private void EmitRescuingEvent()
    {
        EventManager.EmitEvent(EventID.RESCUING_HIDER);
    }
}