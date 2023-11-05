using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ErrorPanel : MonoBehaviour
{
    [SerializeField] private Image _errorPanel;
    [SerializeField] private TMP_Text _errorText;

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
        SetErrorColorAlpha(_minColorAlpha);
    }

    private void LoadComponents()
    {
        _errorPanel = GetComponent<Image>();
        _errorText = GetComponentInChildren<TMP_Text>();
    }

    public void ShowErrorText(string error)
    {
        _errorText.text = error;
        SetErrorColorAlpha(_maxColorAlpha);
        StartCoroutine(FadeErrorPanel());
    }

    IEnumerator FadeErrorPanel()
    {
        int fadeTime = Mathf.CeilToInt(_fadeTime / _fadeAmountTime);
        float aplhaFadeAmount = (_maxColorAlpha - _minColorAlpha) / fadeTime;
        float newAlpha = _maxColorAlpha;

        Debug.Log($"Fade --- Fade Time: {fadeTime} --- Fade Amount: {aplhaFadeAmount} --- New A: {newAlpha}");

        for (int i = 0; i < fadeTime; i++)
        {
            newAlpha -= aplhaFadeAmount;
            SetErrorColorAlpha(newAlpha);

            // Debug.Log($"Fading --- New A: {newAlpha}");

            yield return new WaitForSeconds(_fadeAmountTime);
        }
    }

    private void SetErrorColorAlpha(float alpha)
    {
        var errorPanelColor = _errorPanel.color;
        errorPanelColor.a = alpha;

        _errorPanel.color = errorPanelColor;
        _errorText.alpha = alpha;
    }
}