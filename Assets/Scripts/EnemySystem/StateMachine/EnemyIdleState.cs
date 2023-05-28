using System.Collections;
using UnityEngine;

namespace EnemySystem.StateMachine
{
    public class EnemyIdleState : EnemyBaseState
    {
        private Coroutine _coroutine;
        private bool _isDelayOver;
        private float _distanceToTarget;

        public EnemyIdleState(EnemyStateMachine context, EnemyStateFactory factory) : base(context, factory)
        {
        }

        public override void OnEnterState()
        {
            Debug.Log("Enemy entered in IDLE state.");
            _coroutine = Context.RunCoroutine(Wait());
        }

        public override void OnUpdateState()
        {
            _distanceToTarget = Vector2.Distance(Context.EnemyTransform.position, Context.Target.position);
            CheckSwitchStates();
        }

        public override void OnExitState()
        {
            Debug.Log("Enemy exited from IDLE state.");
            if (_coroutine != null) Context.KillCoroutine(_coroutine);
        }

        public override void CheckSwitchStates()
        {
            if (_distanceToTarget < Context.TriggeredDistance)
            {
                SwitchState(Factory.Chaise());
            }

            if (_isDelayOver)
            {
                SwitchState(Factory.Patrol());
            }
        }

        private IEnumerator Wait()
        {
            yield return new WaitForSeconds(Context.WaitTimeOnPatrolPoint);
            _isDelayOver = true;
        }
    }
}