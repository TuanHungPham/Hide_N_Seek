using UnityEngine;

public class Booster : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";
    private IPickupable _pickupable;

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
        _pickupable = GetComponentInChildren<IPickupable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(PLAYER_TAG)) return;

        _pickupable.DoPickedUpFuction(other.gameObject);
        _pickupable.DestroyItem();
    }
}