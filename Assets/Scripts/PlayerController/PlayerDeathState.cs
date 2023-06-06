using Core.Animation;
using UnityEngine;

namespace PlayerController
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
            _ctx.PlayerEntity.Animator.SetAnimationType(AnimationType.Dead);
            _ctx.PlayerEntity.Animator.PlayAnimation();
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