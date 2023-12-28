using UnityEngine;
using UnityEngine.UI;

public class CatchingPointUI : MonoBehaviour
{
    [SerializeField] private bool _isBright;
    [SerializeField] private bool _isPlusPoint;

    [Space(20)] [SerializeField] private GameObject _plusImage;
    [SerializeField] private Image _pointImg;

    public void SetBright(bool set)
    {
        _isBright = set;
        _pointImg.gameObject.SetActive(set);

        if (_isPlusPoint)
        {
            _plusImage.SetActive(!set);
        }
    }

    public void SetIsPlusPoint(bool set)
    {
        _isPlusPoint = set;
        _plusImage.SetActive(set);
    }

    public bool IsBright()
    {
        return _isBright;
    }

    public bool IsPlusPoint()
    {
        return _isPlusPoint;
    }
}