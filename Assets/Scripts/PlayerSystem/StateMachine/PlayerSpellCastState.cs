using UnityEngine;

namespace PlayerSystem.StateMachine
{
    public class PlayerSpellCastState : PlayerBaseState
    {
        private static readonly int SpellCast = Animator.StringToHash("SpellCast");

        public PlayerSpellCastState(PlayerStateMachine context, 
            PlayerStateFactory playerStateFactory) : base(context, playerStateFactory){}

        public override void EnterState()
        {
            _ctx.Animator.SetBool(SpellCast, true);
        }

        public override void UpdateState()
        {
            CheckSwitchStates();
        }

        public override void ExitState()
        {
            _ctx.Animator.SetBool(SpellCast, false);
        }

        public override void CheckSwitchStates()
        {
            if (_ctx.isMoving)
            {
                SwitchState(_factory.Walk());
            }
            else if(!_ctx.isSpellCasting)
                SwitchState(_factory.Idle());
        }

        public override void InitializeSubState() {}
    }
}
