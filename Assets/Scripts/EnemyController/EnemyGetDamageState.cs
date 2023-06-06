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
            Debug.Log($"Enemy entered in Damage state. Damage: {_takenDamage}");
            if (Context.EnemyEntity.Stats.Armor > 0)
            {
                Context.EnemyEntity.Stats.Armor -= _takenDamage;
            }
            else
            {
                Context.EnemyEntity.Stats.HitPoints -= _takenDamage;
            }
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

            SwitchState(Context.EnemyStateController.CheckIfPlayerInAttackRange() ? Factory.Attack() : Factory.Chaise());
        }
    }
}