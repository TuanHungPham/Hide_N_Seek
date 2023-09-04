using UnityEngine;

public class GameplayTypeButton : MonoBehaviour
{
    private GameplaySystem GameplaySystem => GameplaySystem.Instance;

    public void PlayAsSeeker()
    {
        GameplaySystem.SetGameplayType(true);
    }

    public void PlayAsHider()
    {
        GameplaySystem.SetGameplayType(false);
    }
}