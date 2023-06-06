using StatsSystem;
using UnityEngine;

namespace Entity.Behaviour
{
    public class PlayerEntityBehaviour : BaseEntityBehaviour
    {
        [field: SerializeField] public PlayerStats Stats { get; set; }
        [field: SerializeField] public Transform AttackPoint { get; set; }
        [field: SerializeField] public float AttackPointOffset { get; set; }
    }
}