using System.Collections;
using Core.Movement;
using Entity.Behaviour;
using InventorySystem;
using Pathfinding;
using UnityEngine;

namespace EnemyController
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(EnemyEntityBehaviour))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(EnemyInventory))]
    [RequireComponent(typeof(Seeker))]
    public class EnemyStateMachine : MonoBehaviour
    {
        [field: Header("General")]
        [field: SerializeField]
        public Transform Target { get; set; }

        [field: SerializeField] public LayerMask VisibleLayers { get; set; }
        [field: SerializeField] public float PathUpdateTime { get; set; }

        [field: Header("Patrol")]
        [field: SerializeField]
        public Transform[] PatrolPoints { get; set; }

        [field: SerializeField] public float WaitTimeOnPatrolPoint { get; set; }

        [field: Header("Attack")]
        [field: SerializeField] public LayerMask PlayerLayer { get; set; }
        
        [field: Header("Drop items")]
        [field: SerializeField] public float DropItemsRange { get; private set; }

        public EnemyEntityBehaviour EnemyEntity { get; private set; }
        public PositionMover PositionMover { get; private set; }
        public SeekerController SeekerController { get; private set; }
        public Inventory Inventory { get; private set; }
        public EnemyBaseState CurrentState { get; set; }
        public EnemyStateController EnemyStateController { get; private set; }
        public int LastPatronPointIndex { get; set; }

        private EnemyStateFactory _states;

        private void Awake()
        {
            EnemyEntity = GetComponent<EnemyEntityBehaviour>();
            EnemyEntity.Initialize();

            EnemyStateController = new EnemyStateController();
            EnemyStateController.Initialize(this);

            PositionMover = new PositionMover(EnemyEntity.Rigidbody);

            SeekerController = new SeekerController(GetComponent<Seeker>(), EnemyEntity.Rigidbody);

            _states = new EnemyStateFactory(this);

            Inventory = GetComponent<EnemyInventory>();

            CurrentState = _states.Idle();
            CurrentState.OnEnterState();
        }
        
        private void Update()
        {
            Debug.DrawLine(EnemyEntity.Rigidbody.position,
                EnemyEntity.Rigidbody.position + PositionMover.LastMovementDirection * EnemyEntity.Stats.VisionDistance,
                Color.red);
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

        public void InvokeAnimationEvent()
        {
            if(CurrentState is EnemyAttackState)
                CurrentState.InvokeAnimationEvent();
        }
    }
}