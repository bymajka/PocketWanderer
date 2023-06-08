namespace PlayerController
{
    public class PlayerStateFactory
    {
        private PlayerStateMachine _context;

        public PlayerStateFactory(PlayerStateMachine currentContext)
        {
            _context = currentContext;
        }

        public PlayerBaseState Idle() => new PlayerIdleState(_context, this);
        
        public PlayerBaseState Walk() => new PlayerWalkState(_context, this);
        
        public PlayerBaseState Attack() => new PlayerAttackState(_context, this);
        
        public PlayerBaseState SpellCast() => new PlayerSpellCastState(_context, this);
        
        public PlayerBaseState Death() => new PlayerDeathState(_context, this);
        
        public PlayerBaseState GetDamage(float damage) => new PlayerGetDamageState(_context, this, damage);
    }
}
