namespace PlayerStateMachine
{
    public abstract class PlayerBaseState
    {
        protected PlayerStateMachine _ctx;
        protected PlayerStateFactory _factory;

        protected PlayerBaseState(PlayerStateMachine ctx, PlayerStateFactory factory)
        {
            _ctx = ctx;
            _factory = factory;
        }

        public abstract void EnterState();
        public abstract void UpdateState();
        public abstract void ExitState();
        public abstract void CheckSwitchStates();
        public abstract void InitializeSubState();

        void UpdateStates(){}

        protected void SwitchState(PlayerBaseState newState)
        {
            ExitState();
            newState.EnterState();
            _ctx.PlayerCurrentState = newState;
        }

        protected void SetSuperState() {}
    
        protected void SetSubState(){}
    }
}
