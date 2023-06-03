using UnityEngine;

namespace PlayerSystem.StateMachine
{
    public class PlayerDeathState : PlayerBaseState
    {
        public PlayerDeathState(PlayerStateMachine context, PlayerStateFactory factory)
            : base(context, factory)
        {
        }

        public override void EnterState()
        {
            Debug.Log("Player entered in Damage state.");
            _ctx.Player.PlayDeathAnimation();
        }

        public override void UpdateState()
        {
            CheckSwitchStates();
        }

        public override void ExitState()
        {
            Debug.Log("Player exited from DAMAGE state.");
        }

        public override void CheckSwitchStates()
        {
        }

        public override void InitializeSubState()
        {
        }
    }
}