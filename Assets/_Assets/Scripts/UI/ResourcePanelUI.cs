using System;
using TigerForge;
using TMPro;
using UnityEngine;

public class ResourcePanelUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinText;
    [SerializeField] private TMP_Text _adsTicketText;

    private void Start()
    {
        ListenEvent();
        ShowCoinText();
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.CHOOSING_ITEM_SHOP, ShowCoinText);
    }

    public void ShowCoinText()
    {
        long coin = IngameDataManager.Instance.GetResourceData(eResourceDataType.COIN);
        _coinText.text = coin.ToString();
    }
}