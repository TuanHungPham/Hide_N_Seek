using System;
using System.Collections.Generic;
using UnityEngine;

public class ProductPanel : MonoBehaviour
{
    [SerializeField] private List<IAPPanelUI> _iapPanelUiList = new List<IAPPanelUI>();
    private Dictionary<eProductType, IAPPanelUI> _iapPanelUiDic;
    private IAPSystem IAPSystem => IAPSystem.Instance;

    private void Awake()
    {
        InitializeIAPPanel();
    }

    private void InitializeIAPPanel()
    {
        _iapPanelUiDic = new Dictionary<eProductType, IAPPanelUI>();

        foreach (var iapPanel in _iapPanelUiList)
        {
            _iapPanelUiDic.Add(iapPanel.GetProductTypePanel(), iapPanel);
        }
    }

    private void Start()
    {
        IntializeProductUI();
    }

    public void WatchAds()
    {
        UnityAdsManager.Instance.LoadAds();
        UnityAdsManager.Instance.ShowAds();
    }

    private void IntializeProductUI()
    {
        IAPProductTemplate iapProductTemplate = IAPSystem.GetIAPProductTemplate();
        List<IAPProduct> iapProductList = iapProductTemplate.IAPProductTemplateList;

        foreach (var iapProduct in iapProductList)
        {
            _iapPanelUiDic[iapProduct.productType].AddIAPProductUI(iapProduct);
        }
    }
}