using UnityEngine;

namespace EnemySystem.StateMachine
{
    public class EnemyGetDamageState : EnemyBaseState
    {
        public EnemyGetDamageState(EnemyStateMachine context, EnemyStateFactory factory) : base(context, factory)
        {
        }

        public override void OnEnterState()
        {
            Debug.Log("Enemy entered in Damage state.");
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
            SwitchState(Context.EnemyStateController.CheckIfCanAttack() ? Factory.Attack() : Factory.Chaise());
        }
    }
}