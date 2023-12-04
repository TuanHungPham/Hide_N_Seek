using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour, IPointerClickHandler
{
    #region public

    public UnityAction<ShopItemUI> OnClickItemEvent;

    #endregion

    #region private

    [SerializeField] private bool _isOwned;
    [SerializeField] private bool _isSelected;

    [Space(20)] [SerializeField] private TMP_Text _priceText;
    [SerializeField] private Image _itemImg;
    [SerializeField] private Button _buyButton;
    [SerializeField] private GameObject _selectButton;
    [SerializeField] private ItemShop _itemShop;

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
        _buyButton.onClick.AddListener(InvokeOnClickEvent);
    }

    public void SetUIData(ItemShop itemShop, bool isOwned, bool isSelected)
    {
        _itemShop = itemShop;

        _itemShop.SetOwnedState(isOwned);
        _itemShop.SetSelectState(isSelected);

        _priceText.text = itemShop.GetItemPrice().ToString();
        _itemImg.sprite = itemShop.GetItemImage();

        SetUIState();
    }

    private void SetUIState()
    {
        if (_itemShop.IsOwned())
        {
            _buyButton.gameObject.SetActive(false);
        }
        else
        {
            _buyButton.gameObject.SetActive(true);
        }

        if (_itemShop.IsSelected())
        {
            _selectButton.SetActive(true);
        }
        else
        {
            _selectButton.SetActive(false);
        }
    }

    public void Buy()
    {
        _itemShop.SetOwnedState(true);
        Select();

        GameplayManager.Instance.ConsumeCoin(_itemShop.GetItemPrice());

        _buyButton.gameObject.SetActive(false);
        _selectButton.SetActive(true);
    }

    public void Select()
    {
        _itemShop.SetSelectState(true);

        _selectButton.SetActive(true);
    }

    public void Deselect()
    {
        _itemShop.SetSelectState(false);

        _selectButton.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        InvokeOnClickEvent();
    }

    private void InvokeOnClickEvent()
    {
        OnClickItemEvent?.Invoke(this);
    }

    public ItemShop GetItemShop()
    {
        return _itemShop;
    }

    public int GetItemID()
    {
        return _itemShop.GetItemID();
    }

    public int GetItemPrice()
    {
        return _itemShop.GetItemPrice();
    }

    public bool IsOwned()
    {
        return _itemShop.IsOwned();
    }
}