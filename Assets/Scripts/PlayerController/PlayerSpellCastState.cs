using Core.Animation;
using EnemyController;

namespace PlayerController
{
    public class PlayerSpellCastState : PlayerBaseState
    {
        public PlayerSpellCastState(PlayerStateMachine context,
            PlayerStateFactory playerStateFactory) : base(context, playerStateFactory)
        {
        }

        public override void EnterState()
        {
            _ctx.PlayerEntity.Animator.SetAnimationType(AnimationType.SpellCast);
            _ctx.PlayerEntity.Animator.PlayAnimation();
            DealSpellCastDamage();
        }

        public override void UpdateState()
        {
            CheckSwitchStates();
        }

        public override void ExitState()
        {
            _ctx.PlayerEntity.Animator.StopAnimation();
        }

        public override void CheckSwitchStates()
        {
            if (_ctx.CheckIfDamageTaken(out var damage))
            {
                SwitchState(_factory.GetDamage(damage));
            }

            if (_ctx.isMoving)
            {
                SwitchState(_factory.Walk());
            }
            else if (!_ctx.isSpellCasting)
                SwitchState(_factory.Idle());
        }

        public override void InitializeSubState()
        {
        }
        private void DealSpellCastDamage()
        {
            if (_ctx.PlayerEntity.Stats.ManaPoints < _ctx.PlayerEntity.Stats.spellCastCost)
                return;
            foreach (var enemy in _ctx.ReturnAllEnemies())
            {
                enemy.gameObject.GetComponent<EnemyStateMachine>().EnemyStateController
                    .TakeDamage(_ctx.PlayerEntity.Stats.spellDamage);
            }
            _ctx.PlayerEntity.Stats.ManaPoints -= _ctx.PlayerEntity.Stats.spellCastCost;
        }
    }
}