using UnityEngine;

namespace PlayerController
{
    public class PlayerGetDamageState : PlayerBaseState
    {
        private float _takenDamage;
        
        public PlayerGetDamageState(PlayerStateMachine context, PlayerStateFactory factory, float takenDamage)
            : base(context, factory)
        {
            _takenDamage = takenDamage;
        }

        public override void EnterState()
        {
            Debug.Log($"Player entered in Damage state. Damage: {_takenDamage}");

            if (Ctx.PlayerEntity.Stats.Armor >= _takenDamage)
            {
                Ctx.PlayerEntity.Stats.Armor -= _takenDamage;
            }
            else
            {
                Ctx.PlayerEntity.Stats.HitPoints -= _takenDamage;
            }
        }

        public override void UpdateState()
        {
            CheckSwitchStates();
        }

        public override void ExitState()
        {
            Debug.Log("Player exited from DAMAGE state.");
        }

        public override void CheckSwitchStates()
        {
            var newState = Ctx.PlayerEntity.Stats.HitPoints <= 0 
                ? Factory.Death() 
                : Factory.Idle();
            
            SwitchState(newState);
        }

        public override void InitializeSubState()
        {
        }
    }
}