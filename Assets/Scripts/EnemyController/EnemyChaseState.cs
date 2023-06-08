using System.Collections;
using Core.Animation;
using UnityEngine;

namespace EnemyController
{
    public class EnemyChaseState : EnemyBaseState
    {
        private Coroutine coroutine;
        private Transform target;
        private Vector2 previousTargetPosition;

        public EnemyChaseState(EnemyStateMachine context, EnemyStateFactory factory) : base(context, factory)
        {
        }

        public override void OnEnterState()
        {
            Debug.Log("Enemy entered in CHASE state.");
            Context.EnemyEntity.Animator.SetAnimationType(AnimationType.Move);
            Context.EnemyEntity.Animator.PlayAnimation();
            target = Context.Target;
            coroutine = Context.StartCoroutine(SearchPathCoroutine());
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

            Context.KillCoroutine(coroutine);
        }

        public override void CheckSwitchStates()
        {
            if (Context.EnemyStateController.CheckIfTookDamage(out var damage))
            {
                SwitchState(Factory.GetDamage(damage));
            }
            
            if (Context.EnemyStateController.CheckIfPlayerInAttackRange())
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
            while (target != null)
            {
                Vector2 destination = target.transform.position;
                if (destination != previousTargetPosition)
                {
                    previousTargetPosition = destination;
                    Context.SeekerController.CalculatePath(destination);
                }

                yield return new WaitForSeconds(Context.PathUpdateTime);
            }
        }
    }
}