using UnityEngine;

public class InteractingHandler : MonoBehaviour
{
    [SerializeField] private CharacterInteracting _characterInteracting;
    [SerializeField] private ItemInteracting _itemInteracting;

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
        _characterInteracting = GetComponentInChildren<CharacterInteracting>();
        _itemInteracting = GetComponentInChildren<ItemInteracting>();
    }

    public void CheckCharacterInteracting(GameObject target)
    {
        _characterInteracting.CheckObjColliderState(target);
    }

    public void CheckItemInteracting(GameObject target)
    {
        _itemInteracting.CheckObjColliderState(target);
    }
}