using UnityEngine;

public class ItemInteracting : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private GameObject _coinPickUpVFX;

    public void CheckObjColliderState(GameObject target)
    {
        if (!CanInteract(target)) return;

        _coinPickUpVFX.gameObject.SetActive(true);
    }

    private bool CanInteract(GameObject target)
    {
        if ((_layerMask.value & (1 << target.gameObject.layer)) == 0) return false;

        return true;
    }
}