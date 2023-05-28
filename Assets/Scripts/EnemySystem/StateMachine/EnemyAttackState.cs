using UnityEngine;

namespace EnemySystem.StateMachine
{
    public class EnemyAttackState : EnemyBaseState
    {
        private float _distanceToTarget;

        public EnemyAttackState(EnemyStateMachine context, EnemyStateFactory factory) : base(context, factory)
        {
        }

        public override void OnEnterState()
        {
            Debug.Log("Enemy entered in ATTACK state.");
        }

        public override void OnUpdateState()
        {
            _distanceToTarget = Vector2.Distance(Context.EnemyTransform.position, Context.Target.position);
            CheckSwitchStates();
        }

        public override void OnExitState()
        {
            Debug.Log("Enemy exited from ATTACK state.");
        }

        public override void CheckSwitchStates()
        {
            if (_distanceToTarget > Context.MinChaseDistance)
            {
                SwitchState(Factory.Chaise());
            }
        }
    }
}