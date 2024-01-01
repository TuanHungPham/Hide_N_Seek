using System;
using System.Collections.Generic;
using TigerForge;
using UnityEngine;

public class FrontVision : MonoBehaviour
{
    #region private

    [SerializeField] private Controller _controller;
    [SerializeField] private FieldOfView _fieldOfView;
    [SerializeField] private Light _spotLight;

    #endregion

    private void Awake()
    {
        LoadComponents();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void Start()
    {
        ListenEvent();
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.SPOTTED_OBJECT, HandleGettingSpottedPlayer);
    }

    private void LoadComponents()
    {
        _fieldOfView = GetComponent<FieldOfView>();
        _spotLight = GetComponent<Light>();
    }

    private void HandleGettingSpottedPlayer()
    {
        foreach (var player in _fieldOfView.GetSpottedObjectList())
        {
            Controller controller = player.GetComponent<Controller>();
            if (controller.GetInGameState().IsSeeker() || controller.GetInGameState().IsCaught()) continue;

            _controller = controller;
            EventManager.EmitEvent(EventID.CATCHING_PLAYER);
        }
    }

    public Controller GetCaughtPlayerController()
    {
        return _controller;
    }

    public void SetLightHeight(float height)
    {
        var transformLocalPosition = transform.localPosition;
        transformLocalPosition.y = height;
        transform.localPosition = transformLocalPosition;
    }

    public void SetLightIntensity(float intensity)
    {
        _spotLight.intensity = intensity;
    }

    public void SetLightRange(float range)
    {
        _spotLight.range = range;
    }
}