using Core.Animation;
using PlayerController;
using UnityEngine;

namespace EnemyController
{
    public class EnemyAttackState : EnemyBaseState
    {
        private Vector2 directionToTarget;

        public EnemyAttackState(EnemyStateMachine context, EnemyStateFactory factory) : base(context, factory)
        {
        }

        public override void OnEnterState()
        {
            Debug.Log("Enemy entered in ATTACK state.");
            Context.EnemyEntity.Animator.SetAnimationType(AnimationType.Attack);
            Context.EnemyEntity.Animator.PlayAnimation();
            Attack();
        }

        public override void OnUpdateState()
        {
            directionToTarget = (Context.Target.position - Context.transform.position).normalized;
            Context.PositionMover.LastMovementDirection = directionToTarget;
            Context.EnemyEntity.Animator.SetLastDirection(directionToTarget);
            
            CheckSwitchStates();
            Attack();
        }

        public override void OnFixedUpdateState()
        {
        }

        public override void OnExitState()
        {
            Debug.Log("Enemy exited from ATTACK state.");
            Context.EnemyEntity.Animator.StopAnimation();
        }

        public override void CheckSwitchStates()
        {
            if (Context.EnemyStateController.CheckIfTookDamage(out var damage))
            {
                SwitchState(Factory.GetDamage(damage));
            }

            if (!Context.EnemyStateController.CheckIfPlayerInAttackRange())
            {
                SwitchState(Factory.Chaise());
            }
        }

        private void Attack()
        {
            if (!Context.EnemyStateController.CheckIfCanAttack())
                return;

            var positionFrom = Context.transform.position;
            var positionTo = Context.EnemyEntity.Stats.AttackPointDistance * Context.PositionMover.LastMovementDirection + (Vector2) Context.transform.position;
            var durationSeconds = 3f;
            Debug.DrawLine(positionFrom, positionTo, Color.magenta, durationSeconds);
            
            var hitEnemies = Physics2D.RaycastAll(Context.transform.position,
                Context.PositionMover.LastMovementDirection,
                Context.EnemyEntity.Stats.AttackPointDistance, Context.PlayerLayer);
            
            Debug.Log($"Hit {hitEnemies.Length} enemies.");
            
            foreach (var player in hitEnemies)
            {
                player.transform.GetComponent<PlayerStateMachine>()
                    .TakeDamage(Context.EnemyEntity.Stats.Damage + Context.EnemyEntity.Stats.WeaponDamage);
            }

            Context.EnemyStateController.ResetLastAttackTime();
        }

        public override void InvokeAnimationEvent()
        {
            Attack();
        }
    }
}