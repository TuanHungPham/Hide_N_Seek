using TigerForge;
using UnityEngine;

public class GameplayTypeButton : SSController
{
    private GameplaySystem GameplaySystem => GameplaySystem.Instance;
    [SerializeField] private AudioSource _themeAudio;

    public void PlayAsSeeker()
    {
        GameplaySystem.SetGameplayType(true);
        _themeAudio.Pause();
        EmitStartingGameEvent();
        CloseMainMenu();
    }

    public void PlayAsHider()
    {
        GameplaySystem.SetGameplayType(false);
        _themeAudio.Pause();
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