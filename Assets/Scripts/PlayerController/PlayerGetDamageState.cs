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

            if (_ctx.PlayerEntity.Stats.Armor > 0)
            {
                _ctx.PlayerEntity.Stats.Armor -= _takenDamage;
            }
            else
            {
                _ctx.PlayerEntity.Stats.HitPoints -= _takenDamage;
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
            SwitchState(_ctx.PlayerEntity.Stats.HitPoints <= 0 ? _factory.Death() : _factory.Idle());
        }

        public override void InitializeSubState()
        {
        }
    }
}