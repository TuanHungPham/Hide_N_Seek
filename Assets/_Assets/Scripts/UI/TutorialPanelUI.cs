using System;
using System.Text;
using TigerForge;
using TMPro;
using UnityEngine;

public class TutorialPanelUI : MonoBehaviour
{
    [SerializeField] private GameObject _tutorialPanel;
    [SerializeField] private GameObject _movingTutorialPanel;
    [SerializeField] private TMP_Text _tutorialText;
    [SerializeField] private bool _showedTutorial;
    private const string DATA_KEY = "SHOW_TUTORIAL";
    private GameplaySystem GameplaySystem => GameplaySystem.Instance;

    private void Start()
    {
        _movingTutorialPanel.SetActive(false);
        _tutorialPanel.SetActive(false);

        ListenEvent();
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.STARTING_GAME, ShowTutorial);
        EventManager.StartListening(EventID.GAME_STARTED, DisableTutorial);
    }

    private void DisableTutorial()
    {
        _tutorialPanel.SetActive(false);
        _movingTutorialPanel.SetActive(false);
    }

    private void ShowTutorial()
    {
        _movingTutorialPanel.SetActive(true);

        var data = PlayerPrefs.GetInt(DATA_KEY, 0);
        if (data == 0)
        {
            _showedTutorial = false;
        }
        else
        {
            _showedTutorial = true;
        }

        if (_showedTutorial) return;

        _tutorialPanel.SetActive(true);
        SetTutorialText();
        PlayerPrefs.SetInt(DATA_KEY, 1);
    }

    private void SetTutorialText()
    {
        var requirementNumber = GameplaySystem.GetRequirementNumberOfCaughtHider();

        StringBuilder text = new StringBuilder();
        text.AppendLine($" - Hiders have 3 seconds for hiding. In this time, Seekers can not move.");
        text.AppendLine($" - After 30 seconds, if Seekers catch at least {requirementNumber} or all Hiders, they will win and vice versa.");
        text.AppendLine($" - Player dragging half down of screen for moving Main Character");
        text.AppendLine($" - If Main Character (in Hider Gameplay) is caught, Player can not move until other Hiders rescue Main Character");
        text.AppendLine($" - Main Character collides with other Character for interacting.");

        _tutorialText.text = text.ToString();
    }

    private void OnDestroy()
    {
        EventManager.StopListening(EventID.STARTING_GAME, ShowTutorial);
    }
}