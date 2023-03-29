using UnityEngine;

namespace StateMachine
{
    public class PlayerWalkState : PlayerBaseState
    {
        private static readonly int Moving = Animator.StringToHash("Moving");
        private static readonly int LastHorizontal = Animator.StringToHash("LastHorizontal");
        private static readonly int LastVertical = Animator.StringToHash("LastVertical");
        private static readonly int Horizontal = Animator.StringToHash("Horizontal");
        private static readonly int Vertical = Animator.StringToHash("Vertical");

        public PlayerWalkState(PlayerStateMachine context, 
            PlayerStateFactory playerStateFactory) : base(context, playerStateFactory){}

        public override void EnterState()
        {
            _ctx.Animator.SetBool(Moving, true);
        }

        public override void UpdateState()
        {
            CheckDirection();
            CheckSwitchStates();
            Move();
        }

        public override void ExitState()
        {
            _ctx.isMoving = false;
            _ctx.LastMoveDirection = _ctx.Direction;
            _ctx.Animator.SetBool(Moving, false);
        }

        public override void CheckSwitchStates()
        {
            if (_ctx.isAttacking)
            {
                SwitchState(_factory.Attack());
            }
            else if(_ctx.Direction == Vector2.zero)
            {
                SwitchState(_factory.Idle());
            }
        }

        public override void InitializeSubState() {}

        private void Move()
        {
            _ctx.PlayerRb.position += _ctx.Direction.normalized * (_ctx.Speed * Time.deltaTime);
        }
        
        private void CheckDirection()
        {
            if (_ctx.Direction.x == 0f && _ctx.Direction.y == 0f)
            {
                _ctx.Animator.SetFloat(LastHorizontal, _ctx.LastMoveDirection.x);
                _ctx.Animator.SetFloat(LastVertical, _ctx.LastMoveDirection.y);
            }
            else
            {
                _ctx.LastMoveDirection = _ctx.Direction;
            }
            _ctx.Animator.SetFloat(Horizontal, _ctx.Direction.x);
            _ctx.Animator.SetFloat(Vertical, _ctx.Direction.y);
        }
    }
}
