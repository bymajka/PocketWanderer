using Entity.Behaviour;
using Entity.Movement;
using StatsSystem;
using UnityEngine;

namespace PlayerSystem
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerBehaviour : BaseEntityBehaviour
    {
        [SerializeField] public PlayerStats Stats;
        [SerializeField] public Transform AttackPoint;
        [SerializeField] public float AttackPointOffset;

        public override void Initialize()
        {
            base.Initialize();
            DirectionalMover = new EntityDirectionalMover(Rigidbody, Animator, Stats.MovementSpeed, AttackPoint, AttackPointOffset);
        }
    }
}