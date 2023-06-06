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

        public void Attack()
        {
            var hitEnemies = Physics2D.RaycastAll(_ctx.transform.position, _ctx.DirectionalMover.LastMovementDirection,
                _ctx.PlayerEntity.Stats.AttackPointDistance, _ctx.EnemyLayer);
            Debug.Log(hitEnemies.Length);
            foreach (var enemy in hitEnemies)
            {
                enemy.transform.GetComponent<EnemyStateMachine>().EnemyStateController.TakeDamage(_ctx.PlayerEntity.Stats.Damage);
            }
        }

        public override void ActivateAnimationEvent() => Attack();
    }
}