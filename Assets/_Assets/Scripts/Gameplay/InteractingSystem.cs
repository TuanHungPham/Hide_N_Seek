using System;
using UnityEngine;

public class InteractingSystem : MonoBehaviour
{
    [SerializeField] private Controller _controller;

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
        _controller = GetComponent<Controller>();
    }

    private void OnTriggerEnter(Collider target)
    {
        if (target.gameObject.layer != gameObject.layer) return;

        CheckObjColliderState(target.gameObject);
        Debug.Log("Colliding...........");
    }

    private void CheckObjColliderState(GameObject target)
    {
        Controller targetController = target.GetComponent<Controller>();
        if (targetController == null) return;

        bool targetIsSeeker = targetController.GetInGameState().IsSeeker();
        bool thisObjIsSeeker = _controller.GetInGameState().IsSeeker();

        if (thisObjIsSeeker)
        {
            CollideAsSeeker(targetIsSeeker, targetController);
            return;
        }

        CollideAsHider(targetIsSeeker, targetController);
    }

    private void CollideAsHider(bool targetIsSeeker, Controller targetController)
    {
        if (targetIsSeeker) return;

        if (!targetController.GetInGameState().IsCaught()) return;

        RescueTarget(targetController);
    }

    private void CollideAsSeeker(bool targetIsSeeker, Controller targetController)
    {
        if (targetIsSeeker) return;

        CatchTarget(targetController);
    }

    private void RescueTarget(Controller targetController)
    {
        targetController.SetCaughtState(false);
        Debug.Log("Rescueing...........");
    }

    private void CatchTarget(Controller targetController)
    {
        targetController.SetCaughtState(true);
        Debug.Log("Catching.....................");
    }
}