using TigerForge;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class HidingTimeTextInPlayerCanvas : MonoBehaviour
{
    #region private

    [SerializeField] private Controller _controller;

    [SerializeField] private TMP_Text _hidingTimeText;

    [SerializeField] private float _textY;

    [SerializeField] private float _textX;

    [SerializeField] private float _textZ;

    #endregion

    private void Awake()
    {
        LoadComponents();
        ListenEvent();
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.SETTED_UP_GAMEPLAY, HandleTMPText);
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
    }

    private void Update()
    {
        ShowTimeText();
    }

    private void ShowTimeText()
    {
        if (!_hidingTimeText.gameObject.activeSelf) return;

        float time = GameplaySystem.Instance.GetHidingTimer();

        if (time < 0)
        {
            _hidingTimeText.gameObject.SetActive(false);
            return;
        }

        _hidingTimeText.text = Mathf.CeilToInt(time).ToString();
    }

    private bool CanShowText()
    {
        if (!_controller.GetInGameState().IsSeeker()) return false;

        return true;
    }

    private void HandleTMPText()
    {
        if (!CanShowText())
        {
            _hidingTimeText.gameObject.SetActive(false);
            return;
        }

        _hidingTimeText.gameObject.SetActive(true);

        Vector3 newPos = Camera.main.WorldToScreenPoint(_controller.transform.position);
        // _hidingTimeText.transform.position = new Vector3(newPos.x + _textWidth, newPos.y + _textHeight, newPos.z);
        _hidingTimeText.transform.position = new Vector3(_controller.transform.position.x + _textX,
            _controller.transform.position.y + _textY, _controller.transform.position.z + _textZ);
    }
}