public interface IState
{
    void OnEnterState(StateMachineController stateMachineController);
    void OnUpdateState(StateMachineController stateMachineController);
    void OnFixedUpdateState(StateMachineController stateMachineController);
    void OnExitState(StateMachineController stateMachineController);
    void OnCheckingState(StateMachineController stateMachineController);
}