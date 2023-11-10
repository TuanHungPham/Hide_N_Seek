using UnityEngine;

public class LeaderboardButtonHandler : MonoBehaviour
{
    [SerializeField] private GameObject _winBtnActive;
    [SerializeField] private GameObject _winBtnUnActive;

    [SerializeField] private GameObject _resBtnActive;
    [SerializeField] private GameObject _resBtnUnActive;

    [SerializeField] private GameObject _caughtBtnActive;
    [SerializeField] private GameObject _caughtBtnUnActive;

    public void OnClickWinBtn()
    {
        DisableAllBtn();

        ActiveWinBtnState(true);
    }

    public void OnClickResBtn()
    {
        DisableAllBtn();

        ActiveResBtnState(true);
    }

    public void OnClickCaughtBtn()
    {
        DisableAllBtn();

        ActiveCaughtBtnState(true);
    }

    private void DisableAllBtn()
    {
        ActiveWinBtnState(false);
        ActiveResBtnState(false);
        ActiveCaughtBtnState(false);
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