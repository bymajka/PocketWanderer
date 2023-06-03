using System.Collections;
using UnityEngine;

namespace EnemySystem.StateMachine
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(PathfindingAgent))]
    public class EnemyStateMachine : MonoBehaviour
    {
        [field: Header("General")]
        [field: SerializeField] public Transform Target { get; set; }
        [field: SerializeField] public float MovementSpeed { get; set; }
        [field: SerializeField] public float FOV { get; set; }
        [field: SerializeField] public float VisionDistance { get; set; }
        [field: SerializeField] public float TriggeredDistance { get; set; }
        [field: SerializeField] public LayerMask VisibleLayers { get; set; }

        [field: Header("Patrol")]
        [field: SerializeField] public Transform[] PatrolPoints { get; set; }
        [field: SerializeField] public float WaitTimeOnPatrolPoint { get; set; }

        [field: Header("Attack")]
        [field: SerializeField] public float AttackDistance { get; set; }
        [field: SerializeField] public Transform AttackPoint { get; set; }
        [field: SerializeField] public float AttackPointOffset { get; set; }
        [field: SerializeField] public int AttackDamage { get; set; }
        [field: SerializeField] public float AttackRange { get; set; }
        [field: SerializeField] public LayerMask PlayerLayer { get; set; }

        public EnemyBaseState CurrentState { get; set; }
        public Transform EnemyTransform { get; private set; }
        public Rigidbody2D EnemyRigidbody { get; private set; }
        public Animator Animator { get; private set; }
        public PathfindingAgent PathfindingAgent { get; private set; }
        public EnemyMover EnemyMover { get; private set; }
        public EnemyStateController EnemyStateController { get; private set; }
        public int LastPatronPointIndex { get; set; }

        private EnemyStateFactory _states;


        private void Awake()
        {
            EnemyTransform = transform;
            EnemyRigidbody = GetComponent<Rigidbody2D>();
            PathfindingAgent = GetComponent<PathfindingAgent>();
            Animator = GetComponent<Animator>();
            
            EnemyStateController = new EnemyStateController();
            EnemyMover = new EnemyMover();
            _states = new EnemyStateFactory(this);
            
            EnemyStateController.Initialize(this);
            EnemyMover.Initialize(this);
            
            CurrentState = _states.Idle();
            CurrentState.OnEnterState();
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