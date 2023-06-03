using Assets.Scripts.StatsSystem;
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
            
            var hitEnemies = Physics2D.OverlapCircleAll(Context.AttackPoint.position, Context.AttackRange, Context.PlayerLayer);

            foreach (var enemy in hitEnemies)
            {
                // TODO: remake to do damage
                Debug.Log($"Enemy took damage of {Context.AttackDamage}");
            }
        }

        public override void OnUpdateState()
        {
            CheckSwitchStates();
        }

        public override void OnExitState()
        {
            Debug.Log("Enemy exited from ATTACK state.");
        }

        public override void CheckSwitchStates()
        {
            if (!Context.EnemyStateController.CheckTargetAttackAbility())
            {
                SwitchState(Factory.Chaise());
            }
        }
    }
}