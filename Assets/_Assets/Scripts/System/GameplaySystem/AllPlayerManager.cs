using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class AllPlayerManager : MonoBehaviour
{
    #region private

    [SerializeField] private Transform allPlayerParent;
    [SerializeField] private List<Transform> seekerList = new List<Transform>();
    [SerializeField] private List<Transform> allPlayerList = new List<Transform>();

    #endregion

    private void Awake()
    {
        LoadComponents();
        ListenEvent();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.SPAWNING_PLAYER, InitializePlayerList);
        EventManager.StartListening(EventID.SETTED_UP_GAMEPLAY, HandleGettingSeeker);
    }

    private void InitializePlayerList()
    {
        foreach (Transform child in allPlayerParent)
        {
            if (allPlayerList.Contains(child)) continue;

            allPlayerList.Add(child);
        }

        Debug.Log("Initializing all player List...");
        EmitInitializingPlayerListEvent();
    }

    private void HandleGettingSeeker()
    {
        foreach (Transform obj in allPlayerList)
        {
            Controller controller = obj.GetComponent<Controller>();

            if (!controller.GetInGameState().IsSeeker()) continue;

            seekerList.Add(obj);
        }
    }

    private void EmitInitializingPlayerListEvent()
    {
        EventManager.EmitEvent(EventID.INITIALIZING_ALL_PLAYER_LIST);
    }

    public List<Transform> GetAllPlayerList()
    {
        return allPlayerList;
    }

    public List<Transform> GetSeekerList()
    {
        return seekerList;
    }

    public Vector3 GetNearestSeekerPosition(Transform currentObjCheck)
    {
        float minDistance = Vector3.Distance(currentObjCheck.position, seekerList[0].position);
        Transform nearestSeeker = seekerList[0];

        foreach (var checkingSeeker in seekerList)
        {
            float distance = Vector3.Distance(currentObjCheck.position, checkingSeeker.position);
            if (distance >= minDistance) continue;

            minDistance = distance;
            nearestSeeker = checkingSeeker;
            nearestSeeker.position = checkingSeeker.position;
        }

        // Debug.Log($"{currentObjCheck.name} running from {nearestSeeker.name}");

        return nearestSeeker.position;
    }
}