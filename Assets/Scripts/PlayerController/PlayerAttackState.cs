using Core.Animation;
using EnemyController;
using UnityEngine;

namespace PlayerController
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

            var hitEnemies = Physics2D.OverlapCircleAll(_ctx.AttackPoint.position, _ctx.PlayerEntity.Stats.AttackPointRadius, _ctx.EnemyLayer);

            foreach (var enemy in hitEnemies)
            {
                enemy.GetComponent<EnemyStateMachine>().EnemyStateController.TakeDamage(_ctx.PlayerEntity.Stats.Damage);
            }
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
            if (_ctx.CheckIfDamageTaken(out var damage))
            {
                SwitchState(_factory.GetDamage(damage));
            }
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