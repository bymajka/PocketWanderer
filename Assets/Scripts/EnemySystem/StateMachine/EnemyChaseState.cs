using UnityEngine;

namespace EnemySystem.StateMachine
{
    public class EnemyChaseState : EnemyBaseState
    {
        private static readonly int Moving = Animator.StringToHash("Moving");
        public EnemyChaseState(EnemyStateMachine context, EnemyStateFactory factory) : base(context, factory)
        {
        }

        public override void OnEnterState()
        {
            Debug.Log("Enemy entered in CHASE state.");
            Context.Animator.SetBool(Moving, true);
            Context.PathfindingAgent.Target = Context.Target;
        }

        public override void OnUpdateState()
        {
            Chase();
            CheckSwitchStates();
        }

        public override void OnExitState()
        {
            Debug.Log("Enemy exited from CHASE state.");
            Context.EnemyMover.Stop();
            Context.Animator.SetBool(Moving, false);
        }

        public override void CheckSwitchStates()
        {
            if (Context.EnemyStateController.CheckTargetAttackAbility())
            {
                SwitchState(Factory.Attack());
            }
            
            if (!Context.EnemyStateController.CheckChasePossibility())
            {
                SwitchState(Factory.Idle());
            }
        }

        private void Chase()
        {
            Context.EnemyMover.CheckDirection();
            Context.EnemyMover.Move();
        }
    }
}