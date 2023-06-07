using Core.Movement;
using Entity.Behaviour;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerController
{
    [RequireComponent(typeof(PlayerEntityBehaviour))]
    public class PlayerStateMachine : MonoBehaviour
    {
        [field: Header("Attack")]
        [field: SerializeField] public LayerMask EnemyLayer { get; set; }
        
        public PlayerEntityBehaviour PlayerEntity { get; private set; }
        public DirectionalMover DirectionalMover { get; private set; }
        public PlayerBaseState PlayerCurrentState { get; set; }
        public Vector2 Direction { get; private set; }

        public bool isAttacking;
        public bool isMoving;
        public bool isSpellCasting;

        private PlayerStateFactory _states;
        private float _takenDamage;

        private void Awake()
        {
            _states = new PlayerStateFactory(this);
            PlayerEntity = GetComponent<PlayerEntityBehaviour>();
            PlayerEntity.Initialize();
            DirectionalMover = new DirectionalMover(PlayerEntity.Rigidbody);
            PlayerCurrentState = _states.Idle();
            PlayerCurrentState.EnterState();
        }

        void Update()
        {
            PlayerCurrentState.UpdateState();
        }

        public void TakeDamage(float damage)
        {
           _takenDamage += damage;
        }

        public bool CheckIfDamageTaken(out float damage)
        {
            if (_takenDamage == 0)
            {
                damage = 0;
                return false;
            }

            damage = _takenDamage;
            _takenDamage = 0;
            return true;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            isMoving = true;
            Direction = context.ReadValue<Vector2>();
        }

        public void OnSpellCast(InputAction.CallbackContext context)
        {
            isSpellCasting = true;
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            isAttacking = true;
        }

        public void ActivateAnimationEvent()
        {
            if(PlayerCurrentState is PlayerAttackState)
                PlayerCurrentState.ActivateAnimationEvent();
        }

        public EnemyEntityBehaviour[] ReturnAllEnemies()
        {
            return FindObjectsOfType<EnemyEntityBehaviour>();
        }
    }
}
