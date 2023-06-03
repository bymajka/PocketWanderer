namespace PlayerSystem.StateMachine
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
        public PlayerBaseState ShootingState() => new PlayerShootingState(_context, this);
        public PlayerBaseState MiningState() => new PlayerMiningState(_context, this);
    }
}
