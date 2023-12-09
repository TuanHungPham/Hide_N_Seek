using UnityEngine;

public class PickupableItem : MonoBehaviour
{
    #region private

    [SerializeField] private ePlayerType _targetForGettingItemFunction;
    private IPickupable _pickupable;
    private SoundManager SoundManager => SoundManager.Instance;

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
            _pickupable.DoPickedUpFuction(other.gameObject);
            _pickupable.DestroyItem();
            SoundManager.PlaySFX(eSoundType.COIN_PICKUP, playerController.transform.position);

            return;
        }

        _pickupable.DestroyItem();
    }
}