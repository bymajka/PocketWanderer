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
            Ctx.PlayerEntity.Animator.SetAnimationType(AnimationType.Attack);
            Ctx.PlayerEntity.Animator.PlayAnimation();
        }

        public override void UpdateState()
        {
            CheckSwitchStates();
        }

        public override void ExitState()
        {
            Ctx.PlayerEntity.Animator.StopAnimation();
        }

        public override void CheckSwitchStates()
        {
            if (Ctx.CheckIfDamageTaken(out var damage))
            {
                SwitchState(Factory.GetDamage(damage));
            }

            if (Ctx.isMoving)
            {
                SwitchState(Factory.Walk());
            }
            else if (!Ctx.isAttacking)
            {
                SwitchState(Factory.Idle());
            }
        }

        public override void InitializeSubState()
        {
        }

        private void Attack()
        {
            var hitEnemies = Physics2D.OverlapCircleAll(Ctx.transform.position, Ctx.PlayerEntity.Stats.AttackPointDistance, Ctx.EnemyLayer);
            foreach (var enemyCollider in hitEnemies)
            {
                var enemyStateMachine = enemyCollider.GetComponent<EnemyStateMachine>();
                if (enemyStateMachine != null)
                {
                    enemyStateMachine.EnemyStateController.TakeDamage(Ctx.PlayerEntity.Stats.Damage + Ctx.PlayerEntity.Stats.WeaponDamage);
                }
            }
        }

        public override void ActivateAnimationEvent()
        {
            Attack();
        }
    }
}