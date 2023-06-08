using Core.Animation;
using UnityEngine;

namespace PlayerController
{
    public class PlayerWalkState : PlayerBaseState
    {
        public PlayerWalkState(PlayerStateMachine context, PlayerStateFactory playerStateFactory)
            : base(context, playerStateFactory)
        {
        }

        public override void EnterState()
        {
            Ctx.PlayerEntity.Animator.SetAnimationType(AnimationType.Move);
            Ctx.PlayerEntity.Animator.PlayAnimation();
        }

        public override void UpdateState()
        {
            Ctx.DirectionalMover.Move(Ctx.Direction, Ctx.PlayerEntity.Stats.MovementSpeed * Time.deltaTime);
            Ctx.PlayerEntity.Animator.SetDirection(Ctx.Direction);
            CheckSwitchStates();
        }

        public override void ExitState()
        {
            Ctx.isMoving = false;
            Ctx.PlayerEntity.Animator.SetLastDirection(Ctx.DirectionalMover.LastMovementDirection);
            Ctx.PlayerEntity.Animator.StopAnimation();
        }

        public override void CheckSwitchStates()
        {
            if (Ctx.CheckIfDamageTaken(out var damage))
            {
                SwitchState(Factory.GetDamage(damage));
            }
            if (Ctx.isAttacking)
            {
                SwitchState(Factory.Attack());
            }
            else if (Ctx.Direction == Vector2.zero)
            {
                SwitchState(Factory.Idle());
            }
        }

        public override void InitializeSubState()
        {
        }
    }
}