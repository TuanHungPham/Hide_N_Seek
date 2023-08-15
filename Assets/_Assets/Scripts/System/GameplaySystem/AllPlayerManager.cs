using System.Collections.Generic;
using UnityEngine;
using TigerForge;
using UnityEngine.Serialization;

public class AllPlayerManager : MonoBehaviour
{
    #region private

    [SerializeField] private Transform _allPlayerParent;
    [SerializeField] private List<Transform> _seekerList = new List<Transform>();
    [SerializeField] private List<Transform> _hiderList = new List<Transform>();
    [SerializeField] private List<Transform> _allPlayerList = new List<Transform>();

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
        EventManager.StartListening(EventID.SETTED_UP_GAMEPLAY, HandleGettingPlayerByRole);
    }

    private void InitializePlayerList()
    {
        foreach (Transform child in _allPlayerParent)
        {
            if (_allPlayerList.Contains(child)) continue;

            _allPlayerList.Add(child);
        }

        Debug.Log("Initializing all player List...");
        EmitInitializingPlayerListEvent();
    }

    private void HandleGettingPlayerByRole()
    {
        foreach (Transform obj in _allPlayerList)
        {
            Controller controller = obj.GetComponent<Controller>();

            if (!controller.GetInGameState().IsSeeker())
            {
                AddToHiderList(obj);
                continue;
            }

            AddToSeekerList(obj);
        }
    }

    private void AddToSeekerList(Transform obj)
    {
        _seekerList.Add(obj);
    }

    private void AddToHiderList(Transform obj)
    {
        _hiderList.Add(obj);
    }

    private void EmitInitializingPlayerListEvent()
    {
        EventManager.EmitEvent(EventID.INITIALIZING_ALL_PLAYER_LIST);
    }

    public List<Transform> GetAllPlayerList()
    {
        return _allPlayerList;
    }

    public List<Transform> GetSeekerList()
    {
        return _seekerList;
    }

    public List<Transform> GetHiderList()
    {
        return _hiderList;
    }

    public Vector3 GetNearestSeekerPosition(Transform currentObjCheck)
    {
        float minDistance = Vector3.Distance(currentObjCheck.position, _seekerList[0].position);
        Transform nearestSeeker = _seekerList[0];

        foreach (var checkingSeeker in _seekerList)
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