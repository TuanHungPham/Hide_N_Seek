using System;
using System.Collections.Generic;
using TigerForge;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BigPrizePanelUI : MonoBehaviour
{
    [Header("Image")] [SerializeField] private Image _ticketPrizeImg;
    [SerializeField] private Image _coinPrizeImg;
    [SerializeField] private Image _darkLayer;
    [SerializeField] private Image _checkImg;

    [Header("Text")] [SerializeField] private TMP_Text _ticketPrizeQuantity;
    [SerializeField] private TMP_Text _coinPrizeQuantity;

    [Space(10)] [SerializeField] private SpecialQuest _specialQuest;

    private InGameManager InGameManager => InGameManager.Instance;

    private void Start()
    {
        ListenEvent();
    }

    private void OnEnable()
    {
        InitializeUI();
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.QUEST_RESETTING, InitializeUI);
        EventManager.StartListening(EventID.QUEST_UPDATING, InitializeUI);
    }

    private void InitializeUI()
    {
        _specialQuest = InGameManager.GetTodaySpecialQuest();

        _ticketPrizeImg.sprite = _specialQuest.ticketPrizeIcon;
        _coinPrizeImg.sprite = _specialQuest.coinPrizeIcon;
        _ticketPrizeQuantity.text = _specialQuest.ticketPrizeQuantity.ToString();
        _coinPrizeQuantity.text = _specialQuest.coinPrizeQuantity.ToString();

        if (_specialQuest.isCompleted)
        {
            _darkLayer.gameObject.SetActive(true);
            _checkImg.gameObject.SetActive(true);
        }
        else
        {
            _darkLayer.gameObject.SetActive(false);
            _checkImg.gameObject.SetActive(false);
        }
    }
}