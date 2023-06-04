using Core.Animation;

namespace PlayerStateMachine
{
    public class PlayerAttackState : PlayerBaseState
    {
        public PlayerAttackState(PlayerStateMachine context, PlayerStateFactory playerStateFactory) : base(context, playerStateFactory)
        {
        }

        public override void EnterState()
        {
            _ctx.PlayerEntity.Animator.SetAnimationType(AnimationType.Attack);
            _ctx.PlayerEntity.Animator.PlayAnimation();
        }

        public override void UpdateState()
        {
            CheckSwitchStates();
        }

        public override void ExitState()
        {
            _ctx.PlayerEntity.Animator.StopAnimation();
        }

        public override void CheckSwitchStates()
        {
            if (_ctx.isMoving)
            {
                SwitchState(_factory.Walk());
            }
            else if (!_ctx.isAttacking)
                SwitchState(_factory.Idle());
        }

        public override void InitializeSubState()
        {
        }
    }
}