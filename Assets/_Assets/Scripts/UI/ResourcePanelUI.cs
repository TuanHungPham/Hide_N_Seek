using System;
using TigerForge;
using TMPro;
using UnityEngine;

public class ResourcePanelUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinText;
    [SerializeField] private TMP_Text _adsTicketText;
    private GameplayManager GameplayManager => GameplayManager.Instance;

    private void Start()
    {
        ShowResourcesText();
        ListenEvent();
    }

    private void OnEnable()
    {
        ShowResourcesText();
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.CONSUMING_RESOURCES, ShowResourcesText);
        EventManager.StartListening(EventID.ADDING_RESOURCES, ShowResourcesText);
    }

    public void ShowResourcesText()
    {
        long coin = GameplayManager.GetTotalCoin();
        _coinText.text = coin.ToString();
        long ticket = GameplayManager.GetTotalTicket();
        _adsTicketText.text = ticket.ToString();
    }
}