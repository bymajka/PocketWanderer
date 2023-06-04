using UnityEngine;

namespace PlayerStateMachine
{
    public class PlayerMiningState : PlayerBaseState
    {
        private static readonly int Mine = Animator.StringToHash("Mine");

        public PlayerMiningState(PlayerStateMachine ctx, 
            PlayerStateFactory factory) : base(ctx, factory){}

        public override void EnterState()
        {
            _ctx.Animator.SetBool(Mine, true);
        }

        public override void UpdateState()
        {
            CheckSwitchStates();
        }

        public override void ExitState()
        {
            _ctx.Animator.SetBool(Mine, false);
        }

        public override void CheckSwitchStates()
        {
            if (_ctx.isMoving)
            {
                SwitchState(_factory.Walk());
            }
            else if(!_ctx.isMining)
                SwitchState(_factory.Idle());
        }

        public override void InitializeSubState(){}
    }
}