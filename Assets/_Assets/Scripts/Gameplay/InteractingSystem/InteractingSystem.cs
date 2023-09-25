using System;
using UnityEngine;

public class InteractingSystem : MonoBehaviour
{
    [SerializeField] private InteractingHandler _interactingHandler;

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
    }

    private void OnTriggerEnter(Collider target)
    {
        _interactingHandler.CheckItemInteracting(target.gameObject);
    }

    private void OnTriggerStay(Collider target)
    {
        if (GameplaySystem.Instance.IsInHidingTimer()) return;

        _interactingHandler.CheckCharacterInteracting(target.gameObject);
    }
}