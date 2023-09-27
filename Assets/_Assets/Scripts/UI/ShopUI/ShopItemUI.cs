using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
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

    public void SetUIData(ItemShop itemShop, bool isOwned)
    {
        _isOwned = isOwned;
        _itemShop = itemShop;
        _priceText.text = itemShop.GetItemPrice().ToString();
        _itemImg.sprite = itemShop.GetItemImage();

        if (isOwned)
        {
            _buyButton.gameObject.SetActive(false);
            _selectButton.SetActive(false);
        }
        else
        {
            _buyButton.gameObject.SetActive(true);
            _selectButton.SetActive(false);
        }
    }

    public void Buy()
    {
        _isOwned = true;
        _buyButton.gameObject.SetActive(false);
        _selectButton.SetActive(true);
    }

    public void Select()
    {
        _isSelected = true;
        _selectButton.SetActive(true);
    }

    public void Deselect()
    {
        _isSelected = false;
        _selectButton.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerId == -1)
        {
            InvokeOnClickEvent();
        }
    }

    private void InvokeOnClickEvent()
    {
        OnClickItemEvent?.Invoke(this);
    }

    public ItemShop GetCostumeShop()
    {
        return _itemShop;
    }

    public int GetItemID()
    {
        return _itemShop.GetItemID();
    }

    public bool IsOwned()
    {
        return _isOwned;
    }
}