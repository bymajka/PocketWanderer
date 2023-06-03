using UnityEngine;

namespace EnemySystem.StateMachine
{
    public class EnemyPatrolState : EnemyBaseState
    {
        private int _currentPatrolPointIndex;
        private bool _hasReachedPatrolPoint;

        public EnemyPatrolState(EnemyStateMachine context, EnemyStateFactory factory) : base(context, factory)
        {
        }

        public override void OnEnterState()
        {
            Debug.Log("Enemy entered in PATROL state.");
            Context.Enemy.PlayMovingAnimation();
            _currentPatrolPointIndex = Context.LastPatronPointIndex;
            Context.PathfindingAgent.Target = Context.PatrolPoints[_currentPatrolPointIndex];
        }

        public override void OnUpdateState()
        {
            Patrol();
            CheckSwitchStates();
        }

        public override void OnExitState()
        {
            Debug.Log("Enemy exited from PATROL state.");
            Context.LastPatronPointIndex = _currentPatrolPointIndex;
            Context.Enemy.StopMovingAnimation();
            Context.Enemy.DirectionalMover.LastMovementDirection = Context.Enemy.LastDirection;
            Context.Enemy.DirectionalMover.CheckDirection(Context.Enemy.LastDirection);
            Context.PathfindingAgent.Reset();
        }

        public override void CheckSwitchStates()
        {
            if (Context.EnemyStateController.CheckIfFindTarget(Context.Enemy.Direction))
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
            if (Vector2.Distance(Context.Enemy.transform.position, Context.PatrolPoints[_currentPatrolPointIndex].position) > 0.05f)
            {
                Context.Enemy.Move();
            }
            else
            {
                _hasReachedPatrolPoint = true;
            }
        }
    }
}