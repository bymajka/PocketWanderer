using UnityEngine;

namespace EnemySystem.StateMachine
{
    public class EnemyDeathState : EnemyBaseState
    {
        public EnemyDeathState(EnemyStateMachine context, EnemyStateFactory factory) : base(context, factory)
        {
        }

        public override void OnEnterState()
        {
            Debug.Log("Enemy entered in DEATH state.");
            Context.Enemy.PlayDeathAnimation();
        }

        public override void OnUpdateState()
        {
        }

        public override void OnExitState()
        {
            Debug.Log("Enemy exited from DEATH state.");
        }

        public override void CheckSwitchStates()
        {
        }
    }
}