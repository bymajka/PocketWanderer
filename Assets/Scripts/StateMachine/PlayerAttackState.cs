using Assets.Scripts.StatsSystem;
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
            
            var hitEnemies = Physics2D.OverlapCircleAll(_ctx.AttackPoint.position, _ctx.AttackRange, _ctx.EnemyLayer);

            foreach (var enemy in hitEnemies)
            {
                // TODO: remake to do damage
                Debug.Log($"Enemy took damage of {_ctx.AttackDamage}");
            }
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
