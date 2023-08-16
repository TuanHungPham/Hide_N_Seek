using TigerForge;
using UnityEngine;

public class FrontVision : MonoBehaviour
{
    [SerializeField] private GameObject _thisPlayer;
    [SerializeField] private Controller _controller;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != _thisPlayer.layer) return;


        _controller = other.GetComponent<Controller>();
        EmitCatchingPlayerEvent();
    }

    public Controller GetCaughtPlayerController()
    {
        return _controller;
    }

    private void EmitCatchingPlayerEvent()
    {
        EventManager.EmitEvent(EventID.CATCHING_PLAYER);
    }
}