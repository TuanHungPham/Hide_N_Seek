using TMPro;
using UnityEngine;

public class GameplayTimePanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _timeText;

    private void Update()
    {
        ShowGameplayTime();
    }

    private void ShowGameplayTime()
    {
        float time = GameplaySystem.Instance.GetGameplayTimer();
        float min = Mathf.FloorToInt(time / 60);
        float sec = Mathf.FloorToInt(time % 60);

        _timeText.text = string.Format($"{min}:{sec}");
    }
}