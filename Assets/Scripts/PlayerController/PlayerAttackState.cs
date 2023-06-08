using Core.Animation;
using EnemyController;
using UnityEngine;

namespace PlayerController
{
    public class PlayerAttackState : PlayerBaseState
    {
        public PlayerAttackState(PlayerStateMachine context, PlayerStateFactory playerStateFactory) : base(context,
            playerStateFactory)
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

        private void Attack()
        {
            var hitEnemies = Physics2D.OverlapCircleAll(_ctx.transform.position, _ctx.PlayerEntity.Stats.AttackPointDistance, _ctx.EnemyLayer);
            foreach (var enemyCollider in hitEnemies)
            {
                var enemyStateMachine = enemyCollider.GetComponent<EnemyStateMachine>();
                if (enemyStateMachine != null)
                {
                    enemyStateMachine.EnemyStateController.TakeDamage(_ctx.PlayerEntity.Stats.Damage + _ctx.PlayerEntity.Stats.WeaponDamage);
                }
            }
        }

        public override void ActivateAnimationEvent() => Attack();
    }
}