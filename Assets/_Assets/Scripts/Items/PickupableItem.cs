using UnityEngine;

public class PickupableItem : MonoBehaviour
{
    #region private

    [SerializeField] private PlayerType _targetForGettingItemFunction;
    private IPickupable _pickupable;

    #endregion

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
        _pickupable = GetComponent<IPickupable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Controller playerController = other.GetComponent<Controller>();
        if (playerController.GetPlayerType() == _targetForGettingItemFunction)
        {
            _pickupable.DoPickedUpFuction();
            _pickupable.DestroyItem();
            return;
        }

        _pickupable.DestroyItem();
    }
}