using TigerForge;
using UnityEngine;

public class GameplayTypeButton : SSController
{
    private GameplaySystem GameplaySystem => GameplaySystem.Instance;
    private IngameSetting IngameSetting => IngameSetting.Instance;
    [SerializeField] private AudioSource _themeAudio;

    public override void Start()
    {
        base.Start();
        ListenEvent();
        InitThemeAudio();
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.UPDATE_SETTING, InitThemeAudio);
    }

    private void InitThemeAudio()
    {
        if (IngameSetting.GetSettingState(eSetting.THEME_SOUND))
        {
            _themeAudio.gameObject.SetActive(true);
            return;
        }

        _themeAudio.gameObject.SetActive(false);
    }

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

    private void StopListenEvent()
    {
        EventManager.StopListening(EventID.UPDATE_SETTING, InitThemeAudio);
    }

    public override void OnDisable()
    {
        base.OnDisable();
        StopListenEvent();
    }
}