using UnityEngine;

namespace EnemySystem.StateMachine
{
    public class EnemyPatrolState : EnemyBaseState
    {
        private int _currentPatrolPointIndex;
        private bool _hasReachedPatrolPoint;
        private float _distanceToTarget;

        public EnemyPatrolState(EnemyStateMachine context, EnemyStateFactory factory) : base(context, factory)
        {
        }

        public override void OnEnterState()
        {
            Debug.Log("Enemy entered in PATROL state.");
            _currentPatrolPointIndex = Context.LastPatronPointIndex;
            Context.EnemyMover.Target = Context.PatrolPoints[_currentPatrolPointIndex];
        }

        public override void OnUpdateState()
        {
            _distanceToTarget = Vector2.Distance(Context.EnemyTransform.position, Context.Target.position);
            CheckSwitchStates();
            Patrol();
        }

        public override void OnExitState()
        {
            Debug.Log("Enemy exited from PATROL state.");
            Context.LastPatronPointIndex = _currentPatrolPointIndex;
            Context.EnemyMover.ResetMovement();
        }

        public override void CheckSwitchStates()
        {
            if (_distanceToTarget < Context.TriggeredDistance)
            {
                SwitchState(Factory.Chaise());
            }

            if (!_hasReachedPatrolPoint) return;
            if (_currentPatrolPointIndex + 1 < Context.PatrolPoints.Length)
            {
                _currentPatrolPointIndex++;
            }
            else
            {
                _currentPatrolPointIndex = 0;
            }

            SwitchState(Factory.Idle());
        }

        private void Patrol()
        {
            if (Vector2.Distance(Context.EnemyTransform.position,
                    Context.PatrolPoints[_currentPatrolPointIndex].position) > 0.05f)
            {
                Context.EnemyMover.Move();
            }
            else
            {
                _hasReachedPatrolPoint = true;
            }
        }
    }
}