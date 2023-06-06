using StatsSystem;
using UnityEngine;

namespace Entity.Behaviour
{
    public class EnemyEntityBehaviour : BaseEntityBehaviour
    {
        [field: SerializeField] public EnemyStats Stats { get; set; }
        [field: SerializeField] public Transform AttackPoint { get; set; }
        [field: SerializeField] public float AttackPointOffset { get; set; }
    }
}