using UnityEngine;

namespace PlayerSystem.StateMachine
{
    public class PlayerWalkState : PlayerBaseState
    {
        public PlayerWalkState(PlayerStateMachine context, PlayerStateFactory playerStateFactory) : base(context, playerStateFactory){}

        public override void EnterState()
        {
            _ctx.Player.PlayMovingAnimation();
        }

        public override void UpdateState()
        {
            _ctx.Player.Move(_ctx.Direction);
            CheckSwitchStates();
        }

        public override void ExitState()
        {
            _ctx.isMoving = false;
            _ctx.Player.StopMovingAnimation();
        }

        public override void CheckSwitchStates()
        {
            if (_ctx.CheckIfDamageTaken(out var damage))
            {
                SwitchState(_factory.GetDamage(damage));
            }
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
    }
}
