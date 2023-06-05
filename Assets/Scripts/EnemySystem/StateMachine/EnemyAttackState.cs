using PlayerSystem.StateMachine;
using UnityEngine;

namespace EnemySystem.StateMachine
{
    public class EnemyAttackState : EnemyBaseState
    {
        public EnemyAttackState(EnemyStateMachine context, EnemyStateFactory factory) : base(context, factory)
        {
        }

        public override void OnEnterState()
        {
            Debug.Log("Enemy entered in ATTACK state.");
            Context.Enemy.PlayAttackAnimation();
            Attack();
        }

        public override void OnUpdateState()
        {
            CheckSwitchStates();
            Attack();
        }

        private void Attack()
        {
            if (!Context.EnemyStateController.CheckIfCanAttack())
                return;
            
            var hitPlayers = Physics2D.OverlapCircleAll(Context.AttackPoint.position, Context.Enemy.Stats.AttackPointRadius,
                Context.PlayerLayer);

            foreach (var enemy in hitPlayers)
            {
                enemy.GetComponent<PlayerStateMachine>().TakeDamage(Context.Enemy.Stats.Damage);
            }

            Context.EnemyStateController.ResetLastAttackTime();
        }

        public override void OnExitState()
        {
            Debug.Log("Enemy exited from ATTACK state.");
            Context.Enemy.StopAttackAnimation();
        }

        public override void CheckSwitchStates()
        {
            if (Context.EnemyStateController.CheckIfTookDamage(out var damage))
            {
                SwitchState(Factory.GetDamage(damage));
            }
            if (!Context.EnemyStateController.CheckIfPlayerInAttackRange())
            {
                SwitchState(Factory.Chaise());
            }
        }
    }
}