using System;
using System.Collections.Generic;
using UnityEngine;

public class ProductPanelUI : MonoBehaviour
{
    [SerializeField] private List<IAPPanelUI> _iapPanelUiList = new List<IAPPanelUI>();
    private Dictionary<eProductType, IAPPanelUI> _iapPanelUiDic;
    private IAPSystem IAPSystem => IAPSystem.Instance;
    private UnityAdsManager UnityAdsManager => UnityAdsManager.Instance;

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
        UnityAdsManager.LoadAds();
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