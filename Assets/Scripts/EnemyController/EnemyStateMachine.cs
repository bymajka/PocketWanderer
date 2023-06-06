using System.Collections;
using Core.Movement;
using Entity.Behaviour;
using Pathfinding;
using UnityEngine;

namespace EnemyController
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(EnemyEntityBehaviour))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Seeker))]
    public class EnemyStateMachine : MonoBehaviour
    {
        private EnemyStateFactory _states;

        [field: Header("General")]
        [field: SerializeField] public Transform Target { get; set; }
        [field: SerializeField] public LayerMask VisibleLayers { get; set; }
        [field: SerializeField] public float PathUpdateTime { get; set; }

        [field: Header("Patrol")]
        [field: SerializeField] public Transform[] PatrolPoints { get; set; }
        [field: SerializeField] public float WaitTimeOnPatrolPoint { get; set; }
        
        [field: Header("Attack")]
        [field: SerializeField] public Transform AttackPoint { get; set; }
        [field: SerializeField] public LayerMask PlayerLayer { get; set; }

        public EnemyEntityBehaviour EnemyEntity { get; private set; }
        public PositionMover PositionMover { get; private set; }
        public SeekerController SeekerController { get; private set; }
        public EnemyBaseState CurrentState { get; set; }
        public EnemyStateController EnemyStateController { get; private set; }
        public int LastPatronPointIndex { get; set; }


        private void Awake()
        {
            EnemyEntity = GetComponent<EnemyEntityBehaviour>();
            EnemyEntity.Initialize();

            EnemyStateController = new EnemyStateController();
            EnemyStateController.Initialize(this);

            PositionMover = new PositionMover(EnemyEntity.Rigidbody);
            
            SeekerController = new SeekerController(GetComponent<Seeker>(), EnemyEntity.Rigidbody);

            _states = new EnemyStateFactory(this);
            
            CurrentState = _states.Idle();
            CurrentState.OnEnterState();
        }
        
        private void OnDrawGizmosSelected()
        {
            if (AttackPoint == null)
                return;

            Gizmos.DrawWireSphere(AttackPoint.position, 0.5f);
        }

        private void Update()
        {
            CurrentState.OnUpdateState();
        }

        private void FixedUpdate()
        {
            CurrentState.OnFixedUpdateState();
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