using UnityEngine;

public class GameplayScene : SSController
{
    private static GameplayScene instance;

    public static GameplayScene Instance => instance;

    private new void Awake()
    {
        instance = this;
        SSSceneManager.Instance.PopUp("MainMenu");
    }

    public void CloseScene()
    {
        SSSceneManager.Instance.Close();
    }

    public void ShowWinPopup()
    {
        SSSceneManager.Instance.PopUp("WinScene");
    }

    public void ShowLosePopup()
    {
        SSSceneManager.Instance.PopUp("LoseScene");
    }

    public void ResetScene()
    {
        SSSceneManager.Instance.Reset();
    }
}