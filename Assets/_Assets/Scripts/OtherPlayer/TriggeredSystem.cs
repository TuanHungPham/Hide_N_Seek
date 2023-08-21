using System;
using TigerForge;
using UnityEngine;

public class TriggeredSystem : MonoBehaviour
{
    [SerializeField] private Controller _controller;
    [SerializeField] private FieldOfView _fieldOfView;
    [SerializeField] private HearingSystem _hearingSystem;

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
        _hearingSystem = GetComponentInChildren<HearingSystem>();
    }

    private void Start()
    {
        ListenEvent();
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.SPOTTED_OBJECT, TriggerWhenSeeingFootPrint);
    }

    private void TriggerWhenSeeingFootPrint()
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

    public void SetTriggeredWhenHearingFootstep(bool set)
    {
        if (!CanBeSetTriggered()) return;

        _controller.GetInGameState().SetIsHearingSomething(set);
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

    public Transform GetFootstepSoundMaking()
    {
        return _hearingSystem.GetFootstepSoundMaking();
    }
}