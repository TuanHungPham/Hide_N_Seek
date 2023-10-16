using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    [SerializeField] private Quest _quest;

    [Header("Image UI")] [SerializeField] private Image _questIcon;
    [SerializeField] private Image _questPrizeImg;

    [Header("Text UI")] [SerializeField] private TMP_Text _questDescription;
    [SerializeField] private TMP_Text _questProgressText;
    [SerializeField] private TMP_Text _questPrizeQuantity;
    [SerializeField] private eResourceDataType _questPrizeType;

    [Header("Progress Bar UI")] [SerializeField]
    private RectTransform _questProgressBar;

    [SerializeField] private float maxSizeProgressBar;
    [SerializeField] private float minSizeProgressBar;

    [Header("Prize's Text Color")] [SerializeField]
    private Color _color_1;

    [SerializeField] private Color _color_2;

    public void SetUIData(Quest quest)
    {
        _quest = quest;
        _questIcon.sprite = quest.questIcon;
        _questPrizeImg.sprite = quest.prizeIcon;
        _questDescription.text = quest.questDescription;
        _questProgressText.text = string.Format($"{quest.currentProgress}/{quest.targetProgress}");
        _questPrizeQuantity.text = quest.prizeQuantity.ToString();
        _questPrizeType = quest.prizeType;

        SetUI();
        UpdateUIData(quest.currentProgress, quest.targetProgress);
    }

    public void UpdateUIData(float currentProgress, float targetProgress)
    {
        float progress = currentProgress / targetProgress;
        float newWidth = (maxSizeProgressBar - minSizeProgressBar) * progress;

        Vector2 currentSizeDelta = _questProgressBar.sizeDelta;
        currentSizeDelta.x = newWidth;
        _questProgressBar.sizeDelta = currentSizeDelta;
    }

    private void SetUI()
    {
        if (_questPrizeType == eResourceDataType.ADS_TICKET)
        {
            _questPrizeQuantity.color = _color_1;
            return;
        }

        _questPrizeQuantity.color = _color_2;
    }
}