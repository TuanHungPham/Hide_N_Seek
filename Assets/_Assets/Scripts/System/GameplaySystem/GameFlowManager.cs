using TigerForge;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlowManager : MonoBehaviour
{
    private GameplaySystem GameplaySystem => GameplaySystem.Instance;
    private GameplayScene GameplayScene => GameplayScene.Instance;

    private void Awake()
    {
        ListenEvent();
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.END_GAME_TIME, CheckGameFlowOver);
        EventManager.StartListening(EventID.RETRYING_GAME, ResetGame);
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
    }

    private void SetLose()
    {
        Time.timeScale = 0;
        GameplayScene.ShowLosePopup();
    }

    private void ResetGame()
    {
        Time.timeScale = 1;
        GameplayScene.CloseScene();
        SceneManager.LoadScene("BaseScene");
    }
}