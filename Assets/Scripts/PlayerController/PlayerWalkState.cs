using Core.Animation;
using UnityEngine;

namespace PlayerController
{
    public class PlayerWalkState : PlayerBaseState
    {
        public PlayerWalkState(PlayerStateMachine context, PlayerStateFactory playerStateFactory) : base(context, playerStateFactory)
        {
        }

        public override void EnterState()
        {
            _ctx.PlayerEntity.Animator.SetAnimationType(AnimationType.Move);
            _ctx.PlayerEntity.Animator.PlayAnimation();
        }

        public override void UpdateState()
        {
            _ctx.DirectionalMover.Move(_ctx.Direction, _ctx.PlayerEntity.Stats.MovementSpeed * Time.deltaTime);
            _ctx.PlayerEntity.Animator.SetDirection(_ctx.Direction);
            CheckSwitchStates();
        }

        public override void ExitState()
        {
            _ctx.isMoving = false;
            _ctx.PlayerEntity.Animator.SetLastDirection(_ctx.DirectionalMover.LastMovementDirection);
            _ctx.PlayerEntity.Animator.StopAnimation();
        }

        public override void CheckSwitchStates()
        {
            if (_ctx.CheckIfDamageTaken(out var damage))
            {
                SwitchState(_factory.GetDamage(damage));
            }
            if (_ctx.isAttacking)
            {
                SwitchState(_factory.Attack());
            }
            else if (_ctx.Direction == Vector2.zero)
            {
                SwitchState(_factory.Idle());
            }
        }

        public override void InitializeSubState()
        {
        }
    }
}