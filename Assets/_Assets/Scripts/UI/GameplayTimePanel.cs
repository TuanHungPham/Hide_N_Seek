using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayTimePanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _timeText;
    [SerializeField] private Image _watchImage;
    private GameplaySystem GameplaySystem => GameplaySystem.Instance;

    private void Start()
    {
        _timeText.color = Color.white;
        _watchImage.color = Color.white;
    }

    private void Update()
    {
        ShowGameplayTime();
        ShowWatchImageDepedingOnTime();
    }

    private void ShowGameplayTime()
    {
        float time = GameplaySystem.GetGameplayTimer();
        float min = Mathf.FloorToInt(time / 60);
        float sec = Mathf.CeilToInt(time % 60);

        SetUIPanelColor(time);

        if (sec < 10)
        {
            _timeText.text = string.Format($"{min}:0{sec}");
            return;
        }

        _timeText.text = string.Format($"{min}:{sec}");
    }

    private void ShowWatchImageDepedingOnTime()
    {
        float gameplayTime = GameplaySystem.GetGameplayTime();
        float timer = GameplaySystem.GetGameplayTimer();

        _watchImage.fillAmount = timer / gameplayTime;
    }

    private void SetUIPanelColor(float sec)
    {
        if (sec > 10)
            return;


        if (GameplaySystem.IsSeekerGameplay())
        {
            _timeText.color = Color.red;
            _watchImage.color = Color.red;
            return;
        }

        _timeText.color = Color.green;
        _watchImage.color = Color.green;
    }
}