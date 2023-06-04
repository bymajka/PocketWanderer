using EnemySystem.StateMachine;
using UnityEngine;

namespace PlayerSystem.StateMachine
{
    public class PlayerAttackState : PlayerBaseState
    {
        public PlayerAttackState(PlayerStateMachine context, PlayerStateFactory playerStateFactory) : base(context, playerStateFactory)
        {
        }

        public override void EnterState()
        {
            _ctx.Player.PlayAttackAnimation();
            
            var hitEnemies = Physics2D.OverlapCircleAll(_ctx.AttackPoint.position, _ctx.Player.Stats.AttackPointRadius, _ctx.EnemyLayer);

            foreach (var enemy in hitEnemies)
            {
                enemy.GetComponent<EnemyStateMachine>().EnemyStateController.TakeDamage(_ctx.Player.Stats.Damage);
            }
        }

        public override void UpdateState()
        {
            CheckSwitchStates();
        }

        public override void ExitState()
        {
            _ctx.Player.StopAttackAnimation();
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