using TigerForge;
using UnityEngine;


public class SettingUIPanel : MonoBehaviour
{
    [SerializeField] private GameObject _themeSoundCheck;
    [SerializeField] private GameObject _sfxSoundCheck;
    [SerializeField] private bool _themeSoundOn;
    [SerializeField] private bool _sfxSoundOn;
    private IngameSetting IngameSetting => IngameSetting.Instance;

    private void OnEnable()
    {
        InitializeUI();
    }

    private void InitializeUI()
    {
        _themeSoundOn = IngameSetting.GetSettingState(eSetting.THEME_SOUND);
        _sfxSoundOn = IngameSetting.GetSettingState(eSetting.SFX_SOUND);

        _themeSoundCheck.SetActive(_themeSoundOn);
        _sfxSoundCheck.SetActive(_sfxSoundOn);
    }

    public void SetThemeSoundState()
    {
        _themeSoundOn = !_themeSoundOn;
        _themeSoundCheck.SetActive(_themeSoundOn);

        IngameSetting.SetConfig(eSetting.THEME_SOUND, _themeSoundOn);
        EmitSetupSettingEvent();
    }

    public void SetSFXSoundState()
    {
        _sfxSoundOn = !_sfxSoundOn;
        _sfxSoundCheck.SetActive(_sfxSoundOn);

        IngameSetting.SetConfig(eSetting.SFX_SOUND, _sfxSoundOn);
        EmitSetupSettingEvent();
    }

    private void EmitSetupSettingEvent()
    {
        EventManager.EmitEvent(EventID.UPDATE_SETTING);
    }
}