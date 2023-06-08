namespace PlayerController
{
    public abstract class PlayerBaseState
    {
        protected PlayerStateMachine Ctx;
        protected PlayerStateFactory Factory;

        protected PlayerBaseState(PlayerStateMachine ctx, PlayerStateFactory factory)
        {
            Ctx = ctx;
            Factory = factory;
        }

        public abstract void EnterState();
        public abstract void UpdateState();
        public abstract void ExitState();
        public abstract void CheckSwitchStates();
        public abstract void InitializeSubState();

        protected void SwitchState(PlayerBaseState newState)
        {
            ExitState();
            newState.EnterState();
            Ctx.PlayerCurrentState = newState;
        }

        protected void SetSuperState()
        {
        }

        protected void SetSubState()
        {
        }

        public virtual void ActivateAnimationEvent()
        {
        }
    }
}
