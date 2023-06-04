using System.Collections;
using Core.Animation;
using UnityEngine;

namespace EnemyStateMachine
{
    public class EnemyChaseState : EnemyBaseState
    {
        private Coroutine _coroutine;
        private Transform _target;
        private Vector2 _previousTargetPosition;

        public EnemyChaseState(EnemyStateMachine context, EnemyStateFactory factory) : base(context, factory)
        {
        }

        public override void OnEnterState()
        {
            Debug.Log("Enemy entered in CHASE state.");
            Context.EnemyEntity.Animator.SetAnimationType(AnimationType.Move);
            Context.EnemyEntity.Animator.PlayAnimation();
            _target = Context.Target;
            _coroutine = Context.StartCoroutine(SearchPathCoroutine());
        }

        public override void OnUpdateState()
        {
            Context.EnemyEntity.Animator.SetDirection(Context.PositionMover.LastMovementDirection);
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
            Debug.Log("Enemy exited from CHASE state.");
            Context.EnemyEntity.Animator.SetLastDirection(Context.PositionMover.LastMovementDirection);
            Context.EnemyEntity.Animator.StopAnimation();

            Context.KillCoroutine(_coroutine);
        }

        public override void CheckSwitchStates()
        {
            if (Context.EnemyStateController.CheckIfCanAttack())
            {
                SwitchState(Factory.Attack());
            }

            if (!Context.EnemyStateController.CheckIfCanChase())
            {
                SwitchState(Factory.Idle());
            }
        }

        private IEnumerator SearchPathCoroutine()
        {
            while (_target != null)
            {
                Vector2 destination = _target.transform.position;
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