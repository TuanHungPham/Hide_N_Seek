using TigerForge;

public class GameFlowButtonHandler : SSController
{
    public void Retry()
    {
        ClosePopup();
        EmitRetryGameEvent();
    }

    private void BackToMainMenu()
    {
        SSSceneManager.Instance.PopUp("MainMenu");
    }

    private void ClosePopup()
    {
        SSSceneManager.Instance.Close();
    }

    private void EmitRetryGameEvent()
    {
        EventManager.EmitEvent(EventID.RETRYING_GAME);
    }
}