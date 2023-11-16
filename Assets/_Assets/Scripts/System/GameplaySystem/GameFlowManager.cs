using TigerForge;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlowManager : MonoBehaviour
{
    private GameplaySystem GameplaySystem => GameplaySystem.Instance;
    private GameplayScene GameplayScene => GameplayScene.Instance;

    private InGameManager InGameManager => InGameManager.Instance;

    private void Awake()
    {
        ListenEvent();
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.END_GAME_TIME, CheckGameFlowOver);
        EventManager.StartListening(EventID.RETRYING_GAME, ResetGame);
        EventManager.StartListening(EventID.LOADING_SERVER_DATA, ResetGame);
    }

    private void CheckGameFlowOver()
    {
        if (GameplaySystem.IsSeekerGameplay())
        {
            CheckSeekerGameplayFlow();
            return;
        }

        CheckHiderGameplayFlow();
    }

    private void CheckHiderGameplayFlow()
    {
        int numberOfCaughtHider = GameplaySystem.GetNumberOfCaughtHider();
        int requirementNumberOfCaughtHider = GameplaySystem.GetRequirementNumberOfCaughtHider();

        if (numberOfCaughtHider < requirementNumberOfCaughtHider)
        {
            SetWin();
            return;
        }

        SetLose();
    }

    private void CheckSeekerGameplayFlow()
    {
        int numberOfCaughtHider = GameplaySystem.GetNumberOfCaughtHider();
        int requirementNumberOfCaughtHider = GameplaySystem.GetRequirementNumberOfCaughtHider();

        if (numberOfCaughtHider < requirementNumberOfCaughtHider)
        {
            SetLose();
            return;
        }

        SetWin();
    }

    private void SetWin()
    {
        Time.timeScale = 0;
        GameplayScene.ShowWinPopup();

        AddAchievementPoint(eAchievementType.WINNING_TIME);
        AddAchievementPoint(eAchievementType.COMPLETE_LEVEL_TIME);

        EndLevel();
    }

    private void AddAchievementPoint(eAchievementType type)
    {
        InGameManager.AddAchievementPoint(type);
    }

    private void SetLose()
    {
        Time.timeScale = 0;
        GameplayScene.ShowLosePopup();

        AddAchievementPoint(eAchievementType.COMPLETE_LEVEL_TIME);

        EndLevel();
    }

    private void EndLevel()
    {
        UpdateQuest();
        EmitEndGameEvent();
    }

    private void EmitEndGameEvent()
    {
        EventManager.EmitEvent(EventID.END_GAME);
    }

    private void ResetGame()
    {
        Time.timeScale = 1;
        GameplayScene.ResetScene();
    }

    private void UpdateQuest()
    {
        InGameManager.UpdateQuestProgress(eQuestType.CATCHING, eAchievementType.CATCHING_TIME);
        InGameManager.UpdateQuestProgress(eQuestType.RESCUING, eAchievementType.RESCUING_TIME);
        InGameManager.UpdateQuestProgress(eQuestType.WINNING, eAchievementType.WINNING_TIME);
        InGameManager.UpdateQuestProgress(eQuestType.COMPLETION, eAchievementType.COMPLETE_LEVEL_TIME);
    }
}