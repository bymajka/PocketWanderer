using UnityEngine;

namespace EnemySystem.StateMachine
{
    public class EnemyPatrolState : EnemyBaseState
    {
        private static readonly int Moving = Animator.StringToHash("Moving");
        private int _currentPatrolPointIndex;
        private bool _hasReachedPatrolPoint;

        public EnemyPatrolState(EnemyStateMachine context, EnemyStateFactory factory) : base(context, factory)
        {
        }

        public override void OnEnterState()
        {
            Debug.Log("Enemy entered in PATROL state.");
            _currentPatrolPointIndex = Context.LastPatronPointIndex;
            Context.PathfindingAgent.Target = Context.PatrolPoints[_currentPatrolPointIndex];
            Context.Animator.SetBool(Moving, true);
        }

        public override void OnUpdateState()
        {
            Context.EnemyMover.CheckDirection();
            Patrol();
            CheckSwitchStates();
        }

        public override void OnExitState()
        {
            Debug.Log("Enemy exited from PATROL state.");
            Context.LastPatronPointIndex = _currentPatrolPointIndex;
            Context.Animator.SetBool(Moving, false);
            Context.EnemyMover.Stop();
        }

        public override void CheckSwitchStates()
        {
            if (Context.EnemyStateController.CheckTargetVisibility(Context.EnemyMover.LastMoveDirection))
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
            if (Vector2.Distance(Context.EnemyTransform.position, Context.PatrolPoints[_currentPatrolPointIndex].position) > 0.05f)
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