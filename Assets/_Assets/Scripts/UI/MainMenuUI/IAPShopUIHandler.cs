using TigerForge;
using UnityEngine;

public class IAPShopUIHandler : MonoBehaviour
{
    [SerializeField] private bool _isChecked;
    [SerializeField] private GameObject _exclamationMark;

    private void Start()
    {
        ListenEvent();
        HandleExclamationMark();
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.CLOSING_SHOP, HandleExclamationMark);
    }

    public void CheckIAPShop()
    {
        _isChecked = true;
    }

    private void HandleExclamationMark()
    {
        if (_isChecked)
        {
            _exclamationMark.gameObject.SetActive(false);
            return;
        }

        _exclamationMark.gameObject.SetActive(true);
    }
}