using UnityEngine;
using UnityEngine.UI;

public class SeekerNumberButton : MonoBehaviour
{
    [SerializeField] private bool _isSelected;

    [Space(20)] [SerializeField] private Image _buttonImg;
    [SerializeField] private GameObject _checker;
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _choosedColor;

    public void Select()
    {
        SetupButton(true);
    }

    public void Deselect()
    {
        SetupButton(false);
    }

    private void SetupButton(bool isSelect)
    {
        _isSelected = isSelect;
        _checker.gameObject.SetActive(isSelect);

        if (!isSelect)
        {
            _buttonImg.color = _defaultColor;
            return;
        }

        _buttonImg.color = _choosedColor;
    }
}