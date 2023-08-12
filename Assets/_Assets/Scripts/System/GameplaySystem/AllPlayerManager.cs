using System.Collections.Generic;
using UnityEngine;
using TigerForge;

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

            _seeker = obj;
            return;
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

    public Vector3 GetCurrentSeekerPosition()
    {
        return _seeker.position;
    }
}