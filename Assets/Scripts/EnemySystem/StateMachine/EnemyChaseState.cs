using Pathfinding;
using UnityEngine;

namespace EnemySystem.StateMachine
{
    public class EnemyChaseState : EnemyBaseState
    {
        private Path _path;
        private int _currentWayPoint;
        private float _distanceToTarget;

        public EnemyChaseState(EnemyStateMachine context, EnemyStateFactory factory) : base(context, factory)
        {
        }

        public override void OnEnterState()
        {
            Debug.Log("Enemy entered in CHASE state.");
            Context.EnemyMover.Target = Context.Target;
        }

        public override void OnUpdateState()
        {
            _distanceToTarget = Vector2.Distance(Context.EnemyTransform.position, Context.Target.position);
            CheckSwitchStates();
            Chase();
        }

        public override void OnExitState()
        {
            Debug.Log("Enemy exited from CHASE state.");
            Context.EnemyMover.ResetMovement();
        }

        public override void CheckSwitchStates()
        {
            if (_distanceToTarget <= Context.MinChaseDistance)
            {
                SwitchState(Factory.Attack());
            }
            
            if (_distanceToTarget >= Context.MaxChaseDistance)
            {
                SwitchState(Factory.Idle());
            }
        }

        private void Chase()
        {
            Context.EnemyMover.Move();
        }
    }
}