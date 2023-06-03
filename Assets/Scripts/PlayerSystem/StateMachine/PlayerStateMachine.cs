using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerSystem.StateMachine
{
    [RequireComponent(typeof(PlayerBehaviour))]
    public class PlayerStateMachine : MonoBehaviour
    {
        public PlayerBehaviour Player { get; private set; }
        public PlayerBaseState PlayerCurrentState { get; set; }
        public Vector2 Direction { get; private set; }
        
        public Animator Animator { get; private set; }

        public bool isAttacking;
        public bool isMoving;
        public bool isSpellCasting;
        public bool isShooting;
        public bool isMining;

        private PlayerStateFactory _states;

        private void Awake()
        {
            _states = new PlayerStateFactory(this);
            Player = GetComponent<PlayerBehaviour>();
            Player.Initialize();
            Animator = GetComponent<Animator>();
            PlayerCurrentState = _states.Idle();
            PlayerCurrentState.EnterState();
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