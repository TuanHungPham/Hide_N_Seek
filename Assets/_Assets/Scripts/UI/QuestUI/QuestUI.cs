using TigerForge;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    [SerializeField] private Quest _quest;
    [SerializeField] private bool _isCompleted;
    [SerializeField] private Button _adsButton;

    [Header("Image UI")] [SerializeField] private Image _questIcon;
    [SerializeField] private Image _questPrizeImg;
    [SerializeField] private Image _checkImg;
    [SerializeField] private Image _darkLayer;

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

    private InGameManager InGameManager => InGameManager.Instance;
    private UnityAdsManager UnityAdsManager => UnityAdsManager.Instance;

    private void OnEnable()
    { 
        EventManager.StartListening(EventID.SHOWING_ADS_FAIL,StopListeningEvent);
        UpdateProgressUIData();
    }

    public void SetUIData(Quest quest)
    {
        _quest = quest;
        _questIcon.sprite = quest.questIcon;
        _questPrizeImg.sprite = quest.prizeIcon;
        _questDescription.text = quest.questDescription;
        _questPrizeQuantity.text = quest.prizeQuantity.ToString();
        _questPrizeType = quest.prizeType;

        SetUI();
        UpdateProgressUIData();
    }

    public void UpdateProgressUIData()
    {
        _isCompleted = _quest.isCompleted;

        if (_isCompleted)
        {
            _adsButton.gameObject.SetActive(false);
            _checkImg.gameObject.SetActive(true);
            _darkLayer.gameObject.SetActive(true);
        }
        else
        {
            _adsButton.gameObject.SetActive(true);
            _checkImg.gameObject.SetActive(false);
            _darkLayer.gameObject.SetActive(false);
        }

        var currentProgress = _quest.currentProgress;
        var targetProgress = _quest.targetProgress;

        _questProgressText.text = string.Format($"{currentProgress}/{targetProgress}");

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

    public void WatchAds()
    {
        ListenEvent();
        UnityAdsManager.ShowAds();
    }

    private void SkipQuest()
    {
        _quest.FinishQuest();
        UpdateProgressUIData();

        InGameManager.FinishQuest(_quest.questID);
        StopListeningEvent();
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.SHOWING_ADS_COMPLETE,this.SkipQuest);
    }

    private void StopListeningEvent()
    {
        EventManager.StopListening(EventID.SHOWING_ADS_COMPLETE,SkipQuest);
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventID.SHOWING_ADS_FAIL,StopListeningEvent);
        StopListeningEvent();
    }
}