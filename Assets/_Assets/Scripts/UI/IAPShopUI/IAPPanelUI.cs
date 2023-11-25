using System.Collections.Generic;
using UnityEngine;

public class IAPPanelUI : MonoBehaviour
{
    [SerializeField] private List<IAPProductUI> _iapProductList;
    [SerializeField] private eProductType _productType;
    [SerializeField] private IAPProductUI _iapProductUIPrefab;

    public void AddIAPProductUI(IAPProduct iapProduct)
    {
        IAPProductUI iapProductUI = Instantiate(_iapProductUIPrefab, transform, true);

        string productID = iapProduct.productID;
        eProductType productType = iapProduct.productType;
        Sprite productImg = iapProduct.productImage;
        string productQuantity = iapProduct.quantity.ToString();

        iapProductUI.SetData(productID, productType, productImg, productQuantity);
        _iapProductList.Add(iapProductUI);
    }

    public eProductType GetProductTypePanel()
    {
        return _productType;
    }
}