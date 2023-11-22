using Unity.VisualScripting;
using UnityEngine;

public class GameplayScene : SSController
{
    private static GameplayScene instance;

    public static GameplayScene Instance => instance;

    private new void Awake()
    {
        instance = this;
        OpenLoadingScene();
    }

    private void Start()
    {
        SSSceneManager.Instance.PopUp("MainMenu");
    }

    public void OpenLoadingScene()
    {
        SSSceneManager.Instance.ShowLoading(0.5f, 1f);
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

    public void ShowTestPopup()
    {
        SSSceneManager.Instance.PopUp("TestFunctionScene");
    }

    public void ResetScene()
    {
        SSSceneManager.Instance.Reset();
    }
}