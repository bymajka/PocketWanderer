using System.Collections;
using Pathfinding;
using UnityEngine;

namespace EnemySystem.StateMachine
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Seeker))]
    public class EnemyStateMachine : MonoBehaviour
    {
        [field: Header("General")]
        [field: SerializeField] public Transform Target { get; set; }
        [field: SerializeField] public float MovementSpeed { get; set; }
        [field: SerializeField] public float TriggeredDistance { get; set; }
        [field: SerializeField] public float PathUpdateTime { get; set; }

        [field: Header("Chase")]
        [field: SerializeField]
        public float MinChaseDistance { get; set; }

        [field: SerializeField] public float MaxChaseDistance { get; set; }

        [field: Header("Patrol")]
        [field: SerializeField]
        public Transform[] PatrolPoints { get; set; }

        [field: SerializeField] public float WaitTimeOnPatrolPoint { get; set; }

        public EnemyBaseState CurrentState { get; set; }
        
        public Transform EnemyTransform { get; private set; }
        public Rigidbody2D EnemyRigidbody { get; private set; }
        public Seeker Seeker { get; private set; }
        public EnemyMover EnemyMover { get; private set; }

        public int LastPatronPointIndex { get; set; }

        private EnemyStateFactory _states;


        private void Awake()
        {
            EnemyTransform = transform;
            EnemyRigidbody = GetComponent<Rigidbody2D>();
            Seeker = GetComponent<Seeker>();
            EnemyMover = new EnemyMover(this);

            _states = new EnemyStateFactory(this);

            CurrentState = _states.Patrol();
            CurrentState.OnEnterState();
        }

        private void Update()
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