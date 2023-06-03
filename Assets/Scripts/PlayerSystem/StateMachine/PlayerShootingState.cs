using UnityEngine;

namespace PlayerSystem.StateMachine
{
    public class PlayerShootingState : PlayerBaseState
    {
        private static readonly int Shoot = Animator.StringToHash("Shoot");

        public PlayerShootingState(PlayerStateMachine ctx, 
            PlayerStateFactory factory) : base(ctx, factory) {}

        public override void EnterState()
        {
            _ctx.Animator.SetBool(Shoot, true);
        }

        public override void UpdateState()
        {
            CheckSwitchStates();
        }

        public override void ExitState()
        {
            _ctx.Animator.SetBool(Shoot, false);
        }

        public override void CheckSwitchStates()
        {
            if (_ctx.isMoving)
            {
                SwitchState(_factory.Walk());
            }
            else if(!_ctx.isShooting)
                SwitchState(_factory.Idle());
        }

        public override void InitializeSubState(){}
    }
}