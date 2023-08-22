using System.Collections.Generic;
using TigerForge;
using TMPro;
using UnityEngine;

public class HidingTimeTextInPlayerCanvas : MonoBehaviour
{
    #region private

    [SerializeField] private float _textY;

    [SerializeField] private float _textX;

    [SerializeField] private float _textZ;

    [Space(20)] [SerializeField] private GameObject _timeTextPrefab;
    [SerializeField] private List<TMP_Text> textList = new List<TMP_Text>();

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
        float time = GameplaySystem.Instance.GetHidingTimer();

        foreach (var text in textList)
        {
            if (time < 0)
            {
                text.gameObject.SetActive(false);
                return;
            }

            text.text = Mathf.CeilToInt(time).ToString();
        }
    }

    private void HandleTMPText()
    {
        List<Transform> seekerList = GameplaySystem.Instance.GetAllPlayerManager().GetSeekerList();

        foreach (var seeker in seekerList)
        {
            var pos = seeker.position;

            GameObject _hidingTimeText = Instantiate(_timeTextPrefab, transform);
            _hidingTimeText.transform.position = new Vector3(pos.x + _textX,
                pos.y + _textY, pos.z + _textZ);

            TMP_Text text = _hidingTimeText.GetComponent<TMP_Text>();
            textList.Add(text);
        }
    }
}