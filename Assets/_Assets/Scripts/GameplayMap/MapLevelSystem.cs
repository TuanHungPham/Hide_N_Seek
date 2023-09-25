using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MapLevelSystem : MonoBehaviour
{
    private static MapLevelSystem instance;
    public static MapLevelSystem Instance => instance;

    #region private

    [SerializeField] private Vector3 _mainPointPos;

    [SerializeField] private Transform _patrolPointPool;

    [SerializeField] private Transform _startingPointPool;

    [SerializeField] private List<Vector3> _patrolPointList;
    [SerializeField] private List<Vector3> _startingPointList;

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
        InitializePosList(_patrolPointList, _patrolPointPool);
        InitializePosList(_startingPointList, _startingPointPool);
    }

    private void InitializePosList(List<Vector3> list, Transform parent)
    {
        foreach (Transform child in parent)
        {
            if (list.Contains(child.position) || !child.gameObject.activeSelf) continue;

            list.Add(child.position);
        }
    }

    public List<Vector3> GetPatrolPointList()
    {
        return _patrolPointList;
    }

    public List<Vector3> GetStartingPointList()
    {
        return _startingPointList;
    }

    public Vector3 GetMainPointPos()
    {
        return _mainPointPos;
    }

    public int GetStartingPointListCount()
    {
        return _startingPointList.Count;
    }
}