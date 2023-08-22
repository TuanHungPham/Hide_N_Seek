using System;
using UnityEngine;

public class PaintOnGround : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
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
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerStay(Collider other)
    {
        if ((_layerMask.value & (1 << other.gameObject.layer)) == 0) return;

        Controller controller = other.GetComponent<Controller>();
        controller.GetInGameState().SetFeetIsPainted(true, _meshRenderer.material);
    }
}