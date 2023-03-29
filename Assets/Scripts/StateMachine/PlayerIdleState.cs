namespace StateMachine
{
    public class PlayerIdleState : PlayerBaseState
    {

        public PlayerIdleState(PlayerStateMachine context, 
            PlayerStateFactory playerStateFactory) : base(context, playerStateFactory){}

        public override void EnterState() {}

        public override void UpdateState()
        {
            CheckSwitchStates();
        }

        public override void ExitState() {}

        public override void CheckSwitchStates()
        {
            if (_ctx.isAttacking)
            {
                SwitchState(_factory.Attack());
            } 
            else if (_ctx.isSpellCasting)
            {
                SwitchState(_factory.SpellCast());
            }
            else if (_ctx.isShooting)
            {
                SwitchState(_factory.ShootingState());
            }
            else if (_ctx.isMining)
            {
                SwitchState(_factory.MiningState());
            }
            if (_ctx.isMoving)
            {
                SwitchState(_factory.Walk());
            }
        }

        public override void InitializeSubState() {}
    }
}
