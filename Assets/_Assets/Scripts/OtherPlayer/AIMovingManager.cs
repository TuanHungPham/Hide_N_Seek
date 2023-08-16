using UnityEngine;
using UnityEngine.AI;

public class AIMovingManager : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    [Space(20)] [SerializeField] private AIController _aiController;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Vector3 destination;
    [SerializeField] private Transform currentAIPlayer;
    private IMovingSystemAI _aiSystem;

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

    private void Start()
    {
        SetupMovingType();
        _aiSystem.SetInitialDestination();
    }

    private void Update()
    {
        GetDestination();
    }

    private void FixedUpdate()
    {
        // Debug.Log("Destination: " + _aiSystem.Destination);
        Move(destination);
    }

    private void GetDestination()
    {
        _aiSystem.HandleGettingDestination();
        destination = _aiSystem.Destination;
    }

    private void Move(Vector3 pos)
    {
        if (!CanMove())
        {
            _navMeshAgent.ResetPath();
            return;
        }

        _navMeshAgent.SetDestination(pos);
    }

    private void SetupMovingType()
    {
        bool isSeeker = _aiController.GetInGameState().IsSeeker();

        if (isSeeker)
        {
            _aiSystem = new SeekerPatrollingSystem();
            Debug.Log($"{currentAIPlayer.name} - Seeker Moving System");
        }
        else
        {
            _aiSystem = new HiderMovingSystem();
            Debug.Log($"{currentAIPlayer.name} - Hider Moving System");
        }

        _aiSystem.CurrentAIPlayer = currentAIPlayer;
        _aiSystem.AIController = _aiController;
    }

    private bool CanMove()
    {
        if (_aiController.GetInGameState().IsCaught())
            return false;

        return true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _aiController.GetInGameState().IsSeeker() ? Color.red : Color.blue;

        Gizmos.DrawCube(destination, Vector3.one);
    }
}