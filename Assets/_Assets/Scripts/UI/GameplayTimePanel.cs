using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayTimePanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _timeText;
    [SerializeField] private Image _watchImage;

    private void Update()
    {
        ShowGameplayTime();
        ShowWatchImageDepedingOnTime();
    }

    private void ShowGameplayTime()
    {
        float time = GameplaySystem.Instance.GetGameplayTimer();
        float min = Mathf.FloorToInt(time / 60);
        float sec = Mathf.FloorToInt(time % 60);

        if (sec < 10)
        {
            _timeText.text = string.Format($"{min}:0{sec}");
            return;
        }

        _timeText.text = string.Format($"{min}:{sec}");
    }

    private void ShowWatchImageDepedingOnTime()
    {
        float gameplayTime = GameplaySystem.Instance.GetGameplayTime();
        float timer = GameplaySystem.Instance.GetGameplayTimer();

        _watchImage.fillAmount = timer / gameplayTime;
    }
}