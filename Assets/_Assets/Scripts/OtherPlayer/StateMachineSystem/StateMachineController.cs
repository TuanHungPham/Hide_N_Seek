using UnityEngine;
using UnityEngine.AI;

public class StateMachineController : MonoBehaviour
{
    #region public

    public IState currentState;
    public StationaryState stationaryState = new StationaryState();
    public RunAwayState runAwayState = new RunAwayState();
    public PatrollingState patrollingState = new PatrollingState();
    public ChasingState chasingState = new ChasingState();
    public RescuingState rescuingState = new RescuingState();
    public HearingState hearingState = new HearingState();
    public FindingState findingState = new FindingState();

    #endregion

    #region private

    [SerializeField] private StateID _stateID;

    [Space(20)] [SerializeField] private AIController _aiController;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Transform currentAIPlayer;

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
        _aiController = GetComponentInParent<AIController>();
    }

    private void Start()
    {
        SwitchState(stationaryState);
        currentState.OnEnterState(this);
    }

    private void Update()
    {
        if (currentState == null) return;
        currentState.OnUpdateState(this);
    }

    private void FixedUpdate()
    {
        if (currentState == null) return;
        currentState.OnFixedUpdateState(this);
    }

    public void SwitchState(IState state)
    {
        if (currentState != null)
        {
            currentState.OnExitState(this);
        }

        currentState = state;
        currentState.OnEnterState(this);

        ShowStateID(state);
    }

    private void ShowStateID(IState state)
    {
        if (state == stationaryState)
        {
            _stateID = StateID.STATIONARY;
        }
        else if (state == runAwayState)
        {
            _stateID = StateID.MOVING;
        }
        else if (state == patrollingState)
        {
            _stateID = StateID.PATROLLING;
        }
        else if (state == chasingState)
        {
            _stateID = StateID.CHASING;
        }
        else if (state == rescuingState)
        {
            _stateID = StateID.RESCUING;
        }
        else if (state == hearingState)
        {
            _stateID = StateID.HEARING;
        }
        else if (state == findingState)
        {
            _stateID = StateID.FINDING;
        }
    }

    public AIController GetAIController()
    {
        return _aiController;
    }

    public NavMeshAgent GetNavMeshAgent()
    {
        return _navMeshAgent;
    }

    public Transform GetAIPlayer()
    {
        return currentAIPlayer;
    }
}