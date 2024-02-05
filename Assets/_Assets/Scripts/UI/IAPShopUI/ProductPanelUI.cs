using System;
using System.Collections.Generic;
using TigerForge;
using UnityEngine;
using UnityEngine.Serialization;

public class ProductPanelUI : MonoBehaviour
{
    [SerializeField] private List<IAPPanelUI> _iapPanelUiList = new List<IAPPanelUI>();
    [SerializeField] private long ticketRewardQuantity;
    private Dictionary<eProductType, IAPPanelUI> _iapPanelUiDic;
    private IAPSystem IAPSystem => IAPSystem.Instance;
    private UnityAdsManager UnityAdsManager => UnityAdsManager.Instance;
    private GameplayManager GameplayManager => GameplayManager.Instance;

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

    private void OnEnable()
    {
        EventManager.StartListening(EventID.SHOWING_ADS_FAIL,StopListeningEvent);
    }

    public void WatchAds()
    {
        ListenEvent();
        UnityAdsManager.ShowAds();
    }
    
    private void GiveReward()
    {
        GameplayManager.AddTicket(ticketRewardQuantity);
        StopListeningEvent();
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
    
    private void ListenEvent()
    {
        EventManager.StartListening(EventID.SHOWING_ADS_COMPLETE,GiveReward);
    }

    private void StopListeningEvent()
    {
        EventManager.StopListening(EventID.SHOWING_ADS_COMPLETE,GiveReward);
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventID.SHOWING_ADS_FAIL,StopListeningEvent);
        StopListeningEvent();
    }
}