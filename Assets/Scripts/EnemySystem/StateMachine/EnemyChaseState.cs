using UnityEngine;

namespace EnemySystem.StateMachine
{
    public class EnemyChaseState : EnemyBaseState
    {
        public EnemyChaseState(EnemyStateMachine context, EnemyStateFactory factory) : base(context, factory)
        {
        }

        public override void OnEnterState()
        {
            Debug.Log("Enemy entered in CHASE state.");
            Context.Enemy.PlayMovingAnimation();
            Context.PathfindingAgent.Target = Context.Target;
        }

        public override void OnUpdateState()
        {
            Context.Enemy.Move();
            CheckSwitchStates();
        }

        public override void OnExitState()
        {
            Debug.Log("Enemy exited from CHASE state.");
            Context.Enemy.StopMovingAnimation();
            Context.Enemy.DirectionalMover.LastMovementDirection = Context.Enemy.LastDirection;
            Context.Enemy.DirectionalMover.CheckDirection(Context.Enemy.LastDirection);
            Context.PathfindingAgent.Reset();
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
    }
}