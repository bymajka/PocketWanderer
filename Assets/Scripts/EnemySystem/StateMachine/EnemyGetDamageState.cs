using UnityEngine;

namespace EnemySystem.StateMachine
{
    public class EnemyGetDamageState : EnemyBaseState
    {
        private float _distanceToTarget;
        public EnemyGetDamageState(EnemyStateMachine context, EnemyStateFactory factory) : base(context, factory)
        {
        }

        public override void OnEnterState()
        {
            Debug.Log("Enemy entered in Damage state.");
        }

        public override void OnUpdateState()
        {
            _distanceToTarget = Vector2.Distance(Context.EnemyTransform.position, Context.Target.position);
            CheckSwitchStates();
        }

        public override void OnExitState()
        {
            Debug.Log("Enemy exited from DAMAGE state.");
        }

        public override void CheckSwitchStates()
        {
            if (_distanceToTarget <= Context.MinChaseDistance)
            {
                SwitchState(Factory.Attack());
            }

            if (_distanceToTarget < Context.MaxChaseDistance)
            {
                SwitchState(Factory.Chaise());
            }
            
            SwitchState(Factory.Idle());
        }
    }
}