namespace PlayerController
{
    public class PlayerIdleState : PlayerBaseState
    {
        public PlayerIdleState(PlayerStateMachine context, PlayerStateFactory playerStateFactory)
            : base(context, playerStateFactory){}

        public override void EnterState()
        {
        }

        public override void UpdateState()
        {
            CheckSwitchStates();
        }

        public override void ExitState()
        {
        }

        public override void CheckSwitchStates()
        {
            if (Ctx.CheckIfDamageTaken(out var damage))
            {
                SwitchState(Factory.GetDamage(damage));
            }
            
            if (Ctx.isAttacking)
            {
                SwitchState(Factory.Attack());
            } 
            else if (Ctx.isSpellCasting)
            {
                SwitchState(Factory.SpellCast());
            }
            
            if (Ctx.isMoving)
            {
                SwitchState(Factory.Walk());
            }
        }

        public override void InitializeSubState()
        {
        }
    }
}
