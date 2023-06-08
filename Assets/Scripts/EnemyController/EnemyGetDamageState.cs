using UnityEngine;

namespace EnemyController
{
    public class EnemyGetDamageState : EnemyBaseState
    {
        private float _takenDamage;

        public EnemyGetDamageState(EnemyStateMachine context, EnemyStateFactory factory, float takenDamage)
            : base(context, factory)
        {
            _takenDamage = takenDamage;
        }

        public override void OnEnterState()
        {
            Debug.LogFormat("Enemy entered in Damage state. Damage: {0}", _takenDamage);

            var remainingDamage = Mathf.Max(_takenDamage - Context.EnemyEntity.Stats.Armor, 0);
            Context.EnemyEntity.Stats.Armor = Mathf.Max(Context.EnemyEntity.Stats.Armor - _takenDamage, 0);
            Context.EnemyEntity.Stats.HitPoints = Mathf.Max(Context.EnemyEntity.Stats.HitPoints - remainingDamage, 0);
        }

        public override void OnUpdateState()
        {
            CheckSwitchStates();
        }

        public override void OnFixedUpdateState()
        {
        }

        public override void OnExitState()
        {
            Debug.Log("Enemy exited from DAMAGE state.");
        }

        public override void CheckSwitchStates()
        {
            if (Context.EnemyEntity.Stats.HitPoints <= 0)
            {
                SwitchState(Factory.Death());
                return;
            }

            SwitchState(Context.EnemyStateController.CheckIfPlayerInAttackRange()
                ? Factory.Attack()
                : Factory.Chaise());
        }
    }
}