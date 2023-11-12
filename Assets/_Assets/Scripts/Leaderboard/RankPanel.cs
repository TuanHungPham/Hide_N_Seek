using TMPro;
using UnityEngine;

public class RankPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _rankText;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _pointText;

    public void SetUI(string name, float point, string rank = "")
    {
        if (_rankText != null && !string.IsNullOrEmpty(rank))
        {
            _rankText.text = rank;
        }

        _nameText.text = name;
        _pointText.text = point.ToString();
    }
}