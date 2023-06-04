namespace EnemyStateMachine
{
    public abstract class EnemyBaseState
    {
        protected EnemyStateMachine Context { get; }

        protected EnemyStateFactory Factory { get; }

        protected EnemyBaseState(EnemyStateMachine context, EnemyStateFactory factory)
        {
            Context = context;
            Factory = factory;
        }

        public abstract void OnEnterState();
        public abstract void OnUpdateState();
        public abstract void OnFixedUpdateState();
        public abstract void OnExitState();
        public abstract void CheckSwitchStates();

        protected void SwitchState(EnemyBaseState newState)
        {
            OnExitState();
            newState.OnEnterState();
            Context.CurrentState = newState;
        }
    }
}