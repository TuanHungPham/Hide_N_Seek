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
    [SerializeField] private CostumeShop _costumeShop;

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

    public void SetUIData(CostumeShop costumeShop, bool isOwned)
    {
        _isOwned = isOwned;
        _costumeShop = costumeShop;
        _priceText.text = costumeShop.GetCostumePrice().ToString();
        _itemImg.sprite = costumeShop.GetCostumeImage();

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

    public CostumeShop GetCostumeShop()
    {
        return _costumeShop;
    }

    public bool IsOwned()
    {
        return _isOwned;
    }
}