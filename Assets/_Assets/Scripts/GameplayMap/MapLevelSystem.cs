using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MapLevelSystem : MonoBehaviour
{
    private static MapLevelSystem instance;
    public static MapLevelSystem Instance => instance;

    #region private

    [SerializeField] private Transform mapLimitation;
    [SerializeField] private Transform patrolPointParent;

    [SerializeField] private List<Vector3> limitPosList;
    [SerializeField] private List<Vector3> patrolPointList;

    #endregion

    private void Awake()
    {
        instance = this;
        LoadComponents();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        InitializePosList(limitPosList, mapLimitation);
        InitializePosList(patrolPointList, patrolPointParent);
    }

    private void InitializePosList(List<Vector3> list, Transform parent)
    {
        foreach (Transform child in parent)
        {
            if (list.Contains(child.position)) continue;

            list.Add(child.position);
        }
    }

    public List<Vector3> GetLimitPosList()
    {
        return limitPosList;
    }

    public List<Vector3> GetPatrolPointList()
    {
        return patrolPointList;
    }
}