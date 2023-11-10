using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum eNotiType
{
    ERROR,
    SUCCESS,
}

public class NotiPanel : MonoBehaviour
{
    [SerializeField] private UnityEvent _event;
    [SerializeField] private Image _notiPanel;
    [SerializeField] private TMP_Text _notiText;

    [Space(20)] [Header("Fade System")] [SerializeField]
    private float _fadeTime;

    [SerializeField] private float _fadeAmountTime;
    [SerializeField] private float _maxColorAlpha;
    [SerializeField] private float _minColorAlpha;

    private void Awake()
    {
        LoadComponents();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void OnEnable()
    {
        SetNotiColorAlpha(_minColorAlpha);
    }

    private void LoadComponents()
    {
        _notiPanel = GetComponent<Image>();
        _notiText = GetComponentInChildren<TMP_Text>();
    }

    public void ShowNotification(string noti, eNotiType notiType)
    {
        _notiText.text = noti;
        SetNotiPanelColor(notiType);

        SetNotiColorAlpha(_maxColorAlpha);
        StartCoroutine(FadeNotiPanel());

        if (notiType == eNotiType.ERROR) return;
        Invoke(nameof(TriggerClosingPopupEvent), _fadeTime);
    }

    private void SetNotiPanelColor(eNotiType notiType)
    {
        switch (notiType)
        {
            case eNotiType.ERROR:
                _notiPanel.color = Color.red;
                break;
            case eNotiType.SUCCESS:
                _notiPanel.color = Color.green;
                break;
        }
    }

    IEnumerator FadeNotiPanel()
    {
        int fadeTime = Mathf.CeilToInt(_fadeTime / _fadeAmountTime);
        float aplhaFadeAmount = (_maxColorAlpha - _minColorAlpha) / fadeTime;
        float newAlpha = _maxColorAlpha;

        Debug.Log($"Fade --- Fade Time: {fadeTime} --- Fade Amount: {aplhaFadeAmount} --- New A: {newAlpha}");

        for (int i = 0; i < fadeTime; i++)
        {
            newAlpha -= aplhaFadeAmount;
            SetNotiColorAlpha(newAlpha);

            yield return new WaitForSeconds(_fadeAmountTime);
        }
    }

    private void SetNotiColorAlpha(float alpha)
    {
        var errorPanelColor = _notiPanel.color;
        errorPanelColor.a = alpha;

        _notiPanel.color = errorPanelColor;
        _notiText.alpha = alpha;
    }

    private void TriggerClosingPopupEvent()
    {
        _event?.Invoke();
    }
}