using System;
using TigerForge;
using UnityEngine;

public class TriggeredSystem : MonoBehaviour
{
    [SerializeField] private Controller _controller;
    [SerializeField] private FieldOfView _fieldOfView;

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
        _fieldOfView = GetComponent<FieldOfView>();
    }

    private void Start()
    {
        ListenEvent();
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.SPOTTED_OBJECT, HandleTriggerSystem);
    }

    private void HandleTriggerSystem()
    {
        if (!CanBeSetTriggered()) return;

        foreach (var player in _fieldOfView.GetSpottedObjectList())
        {
            Controller playerController = player.GetComponent<Controller>();
            if (playerController.GetInGameState().IsSeeker() || playerController.GetInGameState().IsCaught()) continue;
            else if (!playerController.GetInGameState().FeetIsPainted()) continue;

            DetectOtherPlayer(playerController);
            TriggerAI();
        }
    }

    private static void DetectOtherPlayer(Controller playerController)
    {
        playerController.SetDetectedState(true);
    }

    private void TriggerAI()
    {
        _controller.SetTriggeredState(true);
    }

    private bool CanBeSetTriggered()
    {
        if (GameplaySystem.Instance.IsInHidingTimer()) return false;
        return true;
    }
}