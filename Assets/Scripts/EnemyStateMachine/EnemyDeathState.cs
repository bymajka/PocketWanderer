using UnityEngine;

namespace EnemyStateMachine
{
    public class EnemyDeathState : EnemyBaseState
    {
        public EnemyDeathState(EnemyStateMachine context, EnemyStateFactory factory) : base(context, factory)
        {
        }

        public override void OnEnterState()
        {
            Debug.Log("Enemy entered in DEATH state.");
        }

        public override void OnUpdateState()
        {
            throw new System.NotImplementedException();
        }

        public override void OnFixedUpdateState()
        {
        }

        public override void OnExitState()
        {
            Debug.Log("Enemy exited from DEATH state.");
        }

        public override void CheckSwitchStates()
        {
            throw new System.NotImplementedException();
        }
    }
}