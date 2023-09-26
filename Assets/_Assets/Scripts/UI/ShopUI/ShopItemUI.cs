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

    [SerializeField] private bool _isSelected;

    [Space(20)] [SerializeField] private TMP_Text _priceText;
    [SerializeField] private Image _itemImg;
    [SerializeField] private GameObject _buyButton;
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
    }

    public void SetUIData(CostumeShop costumeShop, bool isOwned)
    {
        _costumeShop = costumeShop;
        _priceText.text = costumeShop.GetCostumePrice().ToString();
        _itemImg.sprite = costumeShop.GetCostumeImage();

        if (isOwned)
        {
            _buyButton.gameObject.SetActive(false);
            _selectButton.gameObject.SetActive(false);
        }
        else
        {
            _buyButton.gameObject.SetActive(true);
            _selectButton.gameObject.SetActive(false);
        }
    }

    public void Select()
    {
        _isSelected = true;
        _selectButton.gameObject.SetActive(true);
    }

    public void Deselect()
    {
        _isSelected = false;
        _selectButton.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerId == -1)
        {
            OnClickItemEvent?.Invoke(this);
        }
    }

    public CostumeShop GetCostumeShop()
    {
        return _costumeShop;
    }
}