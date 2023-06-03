using UnityEngine;

namespace StatsSystem
{
    [CreateAssetMenu]
    public class EnemyStats : BaseEntityStats
    {
        public float AttackDistance;
        public float AttackCooldown;
        public float VisionDistance;
        public float TriggeredDistance;
        public float FOV;
    }
}