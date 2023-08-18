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
        if (!_hidingTimeText.gameObject.activeSelf) return;

        float time = GameplaySystem.Instance.GetHidingTimer();

        if (time < 0)
        {
            _hidingTimeText.gameObject.SetActive(false);
            _startingInText.gameObject.SetActive(false);
            return;
        }

        _hidingTimeText.text = Mathf.CeilToInt(time).ToString();
    }
}