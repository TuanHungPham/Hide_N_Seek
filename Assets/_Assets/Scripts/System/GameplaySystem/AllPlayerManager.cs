using System.Collections.Generic;
using UnityEngine;

public class AllPlayerManager : MonoBehaviour
{
    #region private

    [SerializeField] private Transform _seeker;

    [SerializeField] private Transform allPlayerParent;
    [SerializeField] private List<Transform> allPlayerList;

    #endregion

    private void Awake()
    {
        LoadComponents();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
    }

    private void Start()
    {
        InitializePlayerList();
    }

    private void InitializePlayerList()
    {
        foreach (Transform child in allPlayerParent)
        {
            if (allPlayerList.Contains(child)) continue;

            allPlayerList.Add(child);
            HandleGettingSeeker(child);
        }
    }

    private void HandleGettingSeeker(Transform obj)
    {
        Controller controller = obj.GetComponent<Controller>();

        if (!controller.GetInGameState().IsSeeker()) return;

        _seeker = obj;
    }

    public List<Transform> GetAllPlayerList()
    {
        return allPlayerList;
    }

    public Vector3 GetCurrentSeekerPosition()
    {
        return _seeker.position;
    }
}