using UnityEngine;
using UnityEngine.InputSystem;

namespace StateMachine
{
    public class PlayerStateMachine : MonoBehaviour
    {
        [field: Header("Movement")]
        [field: SerializeField] public Vector2 Direction { get; set; }

        [field: SerializeField] public float Speed { get; set; }
        public Vector2 LastMoveDirection { get; set; }

        [field: Header("Physics")] 
        [field: SerializeField] public Rigidbody2D PlayerRb { get; set; }

        [field: SerializeField] public Animator Animator { get; set; }

        public PlayerBaseState PlayerCurrentState { get; set; }
    
        private PlayerStateFactory _states;

        public bool isAttacking;
        public bool isMoving;
        public bool isSpellCasting;
        public bool isShooting;
        public bool isMining;
    
        private void Awake()
        {
            _states = new PlayerStateFactory(this);
            PlayerCurrentState = _states.Idle();
            PlayerCurrentState.EnterState();
            PlayerRb = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
        }
    
        void Update()
        {
            PlayerCurrentState.UpdateState();
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

        public void OnShoot(InputAction.CallbackContext context)
        {
            isShooting = true;
        }

        public void OnMining(InputAction.CallbackContext context)
        {
            isMining = true;
        }
    }
}
