using System.Collections;
using Core.Animation;
using UnityEngine;

namespace EnemyController
{
    public class EnemyPatrolState : EnemyBaseState
    {
        private Coroutine _coroutine;
        private Vector2 _previousTargetPosition;
        private int _currentPatrolPointIndex;
        private bool _hasReachedPatrolPoint;

        public EnemyPatrolState(EnemyStateMachine context, EnemyStateFactory factory) : base(context, factory)
        {
        }

        public override void OnEnterState()
        {
            Debug.Log("Enemy entered in PATROL state.");
            Context.EnemyEntity.Animator.SetAnimationType(AnimationType.Move);
            Context.EnemyEntity.Animator.PlayAnimation();

            _currentPatrolPointIndex = Context.LastPatronPointIndex;
            
            _coroutine = Context.StartCoroutine(SearchPathCoroutine());
        }

        public override void OnUpdateState()
        {
            Context.EnemyEntity.Animator.SetDirection(Context.PositionMover.LastMovementDirection);
            
            if (Vector2.Distance(Context.EnemyEntity.transform.position,
                    Context.PatrolPoints[_currentPatrolPointIndex].position) < 0.05f)
            {
                _hasReachedPatrolPoint = true;
            }
            
            CheckSwitchStates();
        }

        public override void OnFixedUpdateState()
        {
            if (!Context.SeekerController.TryGetMoveVector(out var position))
                return;
            Context.PositionMover.Move(position, Context.EnemyEntity.Stats.MovementSpeed * Time.deltaTime);
        }

        public override void OnExitState()
        {
            Debug.Log("Enemy exited from PATROL state.");
            Context.EnemyEntity.Animator.SetLastDirection(Context.PositionMover.LastMovementDirection);
            Context.EnemyEntity.Animator.StopAnimation();

            Context.LastPatronPointIndex = _currentPatrolPointIndex;

            Context.KillCoroutine(_coroutine);
        }

        public override void CheckSwitchStates()
        {
            if (Context.EnemyStateController.CheckIfTookDamage(out var damage))
            {
                SwitchState(Factory.GetDamage(damage));
            }
            
            if (Context.EnemyStateController.CheckIfFindTarget(Context.PositionMover.LastMovementDirection))
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

        private IEnumerator SearchPathCoroutine()
        {
            var target = Context.PatrolPoints[_currentPatrolPointIndex];
            while (!_hasReachedPatrolPoint)
            {
                Vector2 destination = target.transform.position;
                if (destination != _previousTargetPosition)
                {
                    _previousTargetPosition = destination;
                    Context.SeekerController.CalculatePath(destination);
                }

                yield return new WaitForSeconds(Context.PathUpdateTime);
            }
        }
    }
}