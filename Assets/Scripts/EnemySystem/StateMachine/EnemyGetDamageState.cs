using UnityEngine;

namespace EnemySystem.StateMachine
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
            if (Context.Enemy.Stats.Armor > 0)
            {
                Context.Enemy.Stats.Armor -= _takenDamage;
            }
            else
            {
                Context.Enemy.Stats.HitPoints -= _takenDamage;
            }
        }

        public override void OnUpdateState()
        {
            CheckSwitchStates();
        }

        public override void OnExitState()
        {
            Debug.Log("Enemy exited from DAMAGE state.");
        }

        public override void CheckSwitchStates()
        {
            if (Context.Enemy.Stats.HitPoints <= 0)
            {
                SwitchState(Factory.Death());
                return;
            }

            SwitchState(Context.EnemyStateController.CheckIfPlayerInAttackRange() ? Factory.Attack() : Factory.Chaise());
        }
    }
}