public class MainMenuButtonHandler : SSController
{
    public void LoadPopup(string popUpName)
    {
        SSSceneManager.Instance.PopUp(popUpName);
    }

    public void ClosePopup()
    {
        SSSceneManager.Instance.Close();
    }
}