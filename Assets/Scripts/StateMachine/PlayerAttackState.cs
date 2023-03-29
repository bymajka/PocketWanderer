using UnityEngine;

namespace StateMachine
{
    public class PlayerAttackState : PlayerBaseState
    {
        private static readonly int Attack = Animator.StringToHash("Attack");

        public PlayerAttackState(PlayerStateMachine context, 
            PlayerStateFactory playerStateFactory) : base(context, playerStateFactory){}

        public override void EnterState()
        {
            _ctx.Animator.SetBool(Attack, true);
        }

        public override void UpdateState()
        {
            CheckSwitchStates();
        }

        public override void ExitState()
        {
            _ctx.Animator.SetBool(Attack, false);
        }

        public override void CheckSwitchStates()
        {
            if (_ctx.isMoving)
            {
                SwitchState(_factory.Walk());
            }
            else if(!_ctx.isAttacking)
                SwitchState(_factory.Idle());
        }

        public override void InitializeSubState() {}
    }
}
