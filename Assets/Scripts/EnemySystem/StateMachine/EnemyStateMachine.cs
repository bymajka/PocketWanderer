using System.Collections;
using UnityEngine;

namespace EnemySystem.StateMachine
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(EnemyBehaviour))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(PathfindingAgent))]
    public class EnemyStateMachine : MonoBehaviour
    {
        [field: Header("General")]
        [field: SerializeField] public Transform Target { get; set; }
        [field: SerializeField] public LayerMask VisibleLayers { get; set; }

        [field: Header("Patrol")]
        [field: SerializeField] public Transform[] PatrolPoints { get; set; }
        [field: SerializeField] public float WaitTimeOnPatrolPoint { get; set; }
        
        [field: Header("Attack")]
        [field: SerializeField] public Transform AttackPoint { get; set; }
        [field: SerializeField] public LayerMask PlayerLayer { get; set; }

        public EnemyBehaviour Enemy { get; private set; }
        public EnemyBaseState CurrentState { get; set; }
        public PathfindingAgent PathfindingAgent { get; private set; }
        public EnemyStateController EnemyStateController { get; private set; }
        public int LastPatronPointIndex { get; set; }

        private EnemyStateFactory _states;


        private void Awake()
        {
            Enemy = GetComponent<EnemyBehaviour>();
            PathfindingAgent = GetComponent<PathfindingAgent>();

            EnemyStateController = new EnemyStateController();
            _states = new EnemyStateFactory(this);
            
            Enemy.Initialize();
            EnemyStateController.Initialize(this);

            CurrentState = _states.Idle();
            CurrentState.OnEnterState();
        }
        
        private void OnDrawGizmosSelected()
        {
            if (AttackPoint == null)
                return;

            Gizmos.DrawWireSphere(AttackPoint.position, 0.3f);
        }

        private void FixedUpdate()
        {
            CurrentState.OnUpdateState();
        }

        public Coroutine RunCoroutine(IEnumerator coroutine)
        {
            return StartCoroutine(coroutine);
        }

        public void KillCoroutine(Coroutine coroutine)
        {
            if (coroutine != null) StopCoroutine(coroutine);
        }
    }
}