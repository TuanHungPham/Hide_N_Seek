using TMPro;
using UnityEngine;

public class HidingTimePanelUI : MonoBehaviour
{
    #region private

    [SerializeField] private TMP_Text _hidingTimeText;
    [SerializeField] private TMP_Text _startingInText;

    #endregion

    private void Update()
    {
        ShowTimeText();
    }

    private void ShowTimeText()
    {
        if (!GameplaySystem.Instance.IsInHidingTimer()) return;

        float time = GameplaySystem.Instance.GetHidingTimer();

        if (time < 0 || !GameplaySystem.Instance.IsGameStarting())
        {
            SetActiveText(false);
            return;
        }

        SetActiveText(true);
        _hidingTimeText.text = Mathf.CeilToInt(time).ToString();
    }

    private void SetActiveText(bool set)
    {
        _hidingTimeText.gameObject.SetActive(set);
        _startingInText.gameObject.SetActive(set);
    }
}