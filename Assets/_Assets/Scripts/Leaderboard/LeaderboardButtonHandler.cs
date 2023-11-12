using System;
using UnityEngine;

public class LeaderboardButtonHandler : MonoBehaviour
{
    [Header("Button")] [SerializeField] private GameObject _winBtnActive;
    [SerializeField] private GameObject _winBtnUnActive;

    [SerializeField] private GameObject _resBtnActive;
    [SerializeField] private GameObject _resBtnUnActive;

    [SerializeField] private GameObject _caughtBtnActive;
    [SerializeField] private GameObject _caughtBtnUnActive;

    [Space(20)] [Header("Leaderboard Panel")] [SerializeField]
    private LeaderboardPanel _leaderboardPanel;

    public void OnClickWinBtn()
    {
        DisableAllBtn();

        SetLeaderboardType(eLeaderboardType.WIN);
        ActiveWinBtnState(true);
    }

    public void OnClickResBtn()
    {
        DisableAllBtn();

        SetLeaderboardType(eLeaderboardType.RESCUE);
        ActiveResBtnState(true);
    }

    public void OnClickCaughtBtn()
    {
        DisableAllBtn();

        SetLeaderboardType(eLeaderboardType.CATCH);
        ActiveCaughtBtnState(true);
    }

    private void DisableAllBtn()
    {
        ActiveWinBtnState(false);
        ActiveResBtnState(false);
        ActiveCaughtBtnState(false);
    }

    private void SetLeaderboardType(eLeaderboardType type)
    {
        _leaderboardPanel.SetLeaderboardType(type);
    }

    private void ActiveWinBtnState(bool set)
    {
        _winBtnActive.gameObject.SetActive(set);
        _winBtnUnActive.gameObject.SetActive(!set);
    }

    private void ActiveResBtnState(bool set)
    {
        _resBtnActive.gameObject.SetActive(set);
        _resBtnUnActive.gameObject.SetActive(!set);
    }

    private void ActiveCaughtBtnState(bool set)
    {
        _caughtBtnActive.gameObject.SetActive(set);
        _caughtBtnUnActive.gameObject.SetActive(!set);
    }
}