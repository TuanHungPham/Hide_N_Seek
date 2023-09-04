using UnityEngine;

public class GameplayScene : MonoBehaviour
{
    private void Awake()
    {
        SSSceneManager.Instance.PopUp("MainMenu");
    }
}
