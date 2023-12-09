using UnityEngine;

public class SettingUIPanel : MonoBehaviour
{
    [SerializeField] private GameObject _themeSoundCheck;
    [SerializeField] private GameObject _sfxSoundCheck;
    [SerializeField] private bool _themeSoundOn;
    [SerializeField] private bool _sfxSoundOn;

    public void SetThemeSoundState()
    {
        _themeSoundOn = !_themeSoundOn;
        _themeSoundCheck.SetActive(_themeSoundOn);
    }

    public void SetSFXSoundState()
    {
        _sfxSoundOn = !_sfxSoundOn;
        _sfxSoundCheck.SetActive(_sfxSoundOn);
    }
}