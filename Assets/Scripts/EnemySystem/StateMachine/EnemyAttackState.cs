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