using TigerForge;

public class GameplayTypeButton : SSController
{
    private GameplaySystem GameplaySystem => GameplaySystem.Instance;

    public void PlayAsSeeker()
    {
        GameplaySystem.SetGameplayType(true);
        EmitStartingGameEvent();
        CloseMainMenu();
    }

    public void PlayAsHider()
    {
        GameplaySystem.SetGameplayType(false);
        EmitStartingGameEvent();
        CloseMainMenu();
    }

    private void CloseMainMenu()
    {
        SSSceneManager.Instance.Close();
    }

    private void EmitStartingGameEvent()
    {
        EventManager.EmitEvent(EventID.STARTING_GAME);
    }
}