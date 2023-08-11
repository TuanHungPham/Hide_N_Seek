using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovingManager : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    [Space(20)] [SerializeField] private AIController _aiController;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Transform destination;
    [SerializeField] private Transform currentAIPlayer;

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
        _aiController = GetComponentInParent<AIController>();
    }

    private void Update()
    {
        HandleGettingDestination();
    }

    private void FixedUpdate()
    {
        Move(destination.position);
    }

    private void Move(Vector3 pos)
    {
        _navMeshAgent.SetDestination(pos);
    }

    private void HandleGettingDestination()
    {
        if (_aiController.GetInGameState().IsSeeker())
        {
            GetDestinationAsSeeker();
            return;
        }

        GetDestinationAsHider();
    }

    private void GetDestinationAsSeeker()
    {
        List<float> distanceList = new List<float>();

        foreach (Transform player in GameplaySystem.Instance.GetAllPlayerList())
        {
            Controller controller = player.GetComponent<Controller>();

            if (controller.GetInGameState().IsCaught() || controller.GetInGameState().IsSeeker()) continue;

            float distance = Vector3.Distance(currentAIPlayer.position, player.position);
            distanceList.Add(distance);
        }
    }

    private void GetDestinationAsHider()
    {
    }
}