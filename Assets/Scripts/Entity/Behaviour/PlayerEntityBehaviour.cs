using StatsSystem;
using UnityEngine;

namespace Entity.Behaviour
{
    public class PlayerEntityBehaviour : BaseEntityBehaviour
    {
        [field: SerializeField] public PlayerStats Stats { get; set; }
    }
}