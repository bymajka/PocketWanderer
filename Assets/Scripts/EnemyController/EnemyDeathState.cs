using Core.Animation;
using UnityEngine;

namespace EnemyController
{
    public class EnemyDeathState : EnemyBaseState
    {
        public EnemyDeathState(EnemyStateMachine context, EnemyStateFactory factory) : base(context, factory)
        {
        }

        public override void OnEnterState()
        {
            Debug.Log("Enemy entered in DEATH state.");
            Context.EnemyEntity.Animator.SetAnimationType(AnimationType.Dead);
            Context.EnemyEntity.Animator.PlayAnimation();
        }

        public override void OnUpdateState()
        {
        }

        public override void OnFixedUpdateState()
        {
        }

        public override void OnExitState()
        {
            Debug.Log("Enemy exited from DEATH state.");
        }

        public override void CheckSwitchStates()
        {
        }
    }
}