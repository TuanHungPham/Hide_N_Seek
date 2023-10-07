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
        EventManager.StartListening(EventID.CONSUMING_COIN, ShowCoinText);
        EventManager.StartListening(EventID.ADDING_COIN, ShowCoinText);
    }

    public void ShowCoinText()
    {
        long coin = GameplayManager.Instance.GetTotalCoin();
        _coinText.text = coin.ToString();
    }
}