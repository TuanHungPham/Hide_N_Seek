using UnityEngine;

public class NumberOfSeekerButtonManager : MonoBehaviour
{
    private GameplaySystem GameplaySystem => GameplaySystem.Instance;

    public void SetNumberOfSeeker(int number)
    {
        GameplaySystem.SetNumberOfSeeker(number);
    }
}