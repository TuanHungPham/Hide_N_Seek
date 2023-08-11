using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovingManager : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    [Space(20)] [SerializeField] private AIController _aiController;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Vector3 destination;
    [SerializeField] private Transform currentAIPlayer;
    private IAISystem _aiSystem;

    private void Awake()
    {
        LoadComponents();
        SetupMovingType();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        _aiController = GetComponentInParent<AIController>();
    }

    private void Update()
    {
        _aiSystem.HandleGettingDestination();
        GetDestination();
    }

    private void FixedUpdate()
    {
        Debug.Log("Destination: " + _aiSystem.Destination);
        Move(destination);
    }

    private void GetDestination()
    {
        destination = _aiSystem.Destination;
    }

    private void Move(Vector3 pos)
    {
        _navMeshAgent.SetDestination(pos);
    }

    private void SetupMovingType()
    {
        bool isSeeker = _aiController.GetInGameState().IsSeeker();

        if (isSeeker)
        {
            _aiSystem = new SeekerMovingSystem();
            Gizmos.color = Color.red;
        }
        else
        {
            _aiSystem = new HiderMovingSystem();
            Gizmos.color = Color.blue;
        }

        _aiSystem.CurrentAIPlayer = transform.parent;
        _aiSystem._aiController = _aiController;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(destination, Vector3.one);
    }
}