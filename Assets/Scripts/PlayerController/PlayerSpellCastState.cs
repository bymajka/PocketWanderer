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
            Ctx.PlayerEntity.Animator.SetAnimationType(AnimationType.SpellCast);
            Ctx.PlayerEntity.Animator.PlayAnimation();
            DealSpellCastDamage();
        }

        public override void UpdateState()
        {
            CheckSwitchStates();
        }

        public override void ExitState()
        {
            Ctx.PlayerEntity.Animator.StopAnimation();
        }

        public override void CheckSwitchStates()
        {
            if (Ctx.CheckIfDamageTaken(out var damage))
            {
                SwitchState(Factory.GetDamage(damage));
            }

            if (Ctx.isMoving)
            {
                SwitchState(Factory.Walk());
            }
            else if (!Ctx.isSpellCasting)
            {
                SwitchState(Factory.Idle());
            }
        }

        public override void InitializeSubState()
        {
        }
        private void DealSpellCastDamage()
        {
            if (Ctx.PlayerEntity.Stats.ManaPoints < Ctx.PlayerEntity.Stats.spellCastCost)
                return;
            foreach (var enemy in Ctx.ReturnAllEnemies())
            {
                enemy.gameObject.GetComponent<EnemyStateMachine>().EnemyStateController
                    .TakeDamage(Ctx.PlayerEntity.Stats.spellDamage);
            }
            Ctx.PlayerEntity.Stats.ManaPoints -= Ctx.PlayerEntity.Stats.spellCastCost;
        }
    }
}