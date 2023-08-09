using UnityEngine;

public class InGameState : MonoBehaviour
{
    #region private

    [SerializeField] private bool isSeeker;
    [SerializeField] private bool isCaught;

    #endregion

    public bool IsSeeker()
    {
        return isSeeker;
    }

    public bool IsCaught()
    {
        return isCaught;
    }
}