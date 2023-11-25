using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Color = UnityEngine.Color;

public enum eProductType
{
    TICKET,
    COIN,
}

public class IAPProductUI : MonoBehaviour
{
    [SerializeField] private string _productID;
    [SerializeField] private eProductType _productType;
    [SerializeField] private Image _productIcon;
    [SerializeField] private Image _background;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private TMP_Text _productQuantity;

    [Space(20)] [Header("BG Color")] [SerializeField]
    private List<Color> _colorList = new List<Color>();

    public void SetData(string productID, eProductType type, Sprite image, string quantity, string price = "")
    {
        _productID = productID;
        _productType = type;
        _productIcon.sprite = image;
        _priceText.text = price;
        _productQuantity.text = quantity;

        SetBGColor();
    }

    private void SetBGColor()
    {
        int index = (int)_productType;
        _background.color = _colorList[index];
    }
}