namespace EnemySystem.StateMachine
{
    public class EnemyStateFactory
    {
        private readonly EnemyStateMachine _context;

        public EnemyStateFactory(EnemyStateMachine context)
        {
            _context = context;
        }

        public EnemyBaseState Attack() => new EnemyAttackState(_context, this);
        public EnemyBaseState Chaise() => new EnemyChaseState(_context, this);
        public EnemyBaseState Death() => new EnemyDeathState(_context, this);
        public EnemyBaseState GetDamage(float damage) => new EnemyGetDamageState(_context, this, damage);
        public EnemyBaseState Idle() => new EnemyIdleState(_context, this);
        public EnemyBaseState Patrol() => new EnemyPatrolState(_context, this);
    }
}