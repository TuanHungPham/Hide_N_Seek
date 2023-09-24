using System.Collections.Generic;
using TigerForge;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class HidingTimeTextInPlayerCanvas : MonoBehaviour
{
    #region private

    [Space(20)] [SerializeField] private GameObject _timeText;
    [SerializeField] private InGameState _inGameState;

    #endregion

    private void Awake()
    {
        LoadComponents();
        ListenEvent();
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.GAME_STARTED, ShowTimeText);
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
    }

    private void ShowTimeText()
    {
        if (!CanShowTimeText())
        {
            _timeText.gameObject.SetActive(false);
            return;
        }

        _timeText.gameObject.SetActive(true);
    }

    private bool CanShowTimeText()
    {
        if (!GameplaySystem.Instance.IsInHidingTimer() || !_inGameState.IsSeeker()) return false;

        return true;
    }
}